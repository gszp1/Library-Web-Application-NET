using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.exception;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Library_Web_Application_NET.Server.src.service.interfaces;
using Library_Web_Application_NET.Server.src.util;
using Microsoft.IdentityModel.Tokens;

namespace Library_Web_Application_NET.Server.src.service
{
    public class ResourceService : GenericService<Resource>, IResourceService
    {
        public ResourceService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<List<ResourceDto>> GetAllWithAuthorsAsync()
        {
            List<Resource> resources = await unitOfWork.Resources.FindAllWithAuthorsAsync();
            return resources.Select(r => MapToDto(r)).ToList();
        }

        public async Task<PagedResult<ResourceDto>> GetAllWithAuthorsPageableAsync(Pageable pageable)
        {
            PagedResult<Resource> resources = await unitOfWork.Resources.FindAllWithAuthorsPageableAsync(pageable);
            return new PagedResult<ResourceDto>()
            {
                Items = resources.Items.Select(r => MapToDto(r)).ToList(),
                TotalItems = resources.TotalItems,
                PageNumber = resources.PageNumber,
                PageSize = resources.PageSize
            };
        }

        public async Task<ResourceDescriptionDto> GetResourceDescriptionAsync(int id)
        {
            Resource? resource = await unitOfWork
                .Resources
                .FindByIdAsync(id)
                ?? throw new NoSuchRecordException("Resource with given id does not exist.");
            return new ResourceDescriptionDto()
            {
                Description = resource.Descripiton
            };
        }

        public async Task<List<ResourceDto>> GetResourcesWithKeywordInTitleAsync(string keyword)
        {
            List<Resource> resources = await unitOfWork
                .Resources
                .FindAllByTitleKeywordAsync(keyword);
            return resources.Select(r => MapToDto(r)).ToList();
        }

        public async Task<PagedResult<ResourceDto>> GetResourcesWithKeywordInTitlePageableAsync(string keyword, Pageable pageable)
        {
            PagedResult<Resource> resources = await unitOfWork
                .Resources
                .FindAllByTitleKeywordPageableAsync(keyword, pageable);
            return new PagedResult<ResourceDto>()
            {
                TotalItems = resources.TotalItems,
                PageNumber = resources.PageNumber,
                PageSize = resources.PageSize,
                Items = resources.Items.Select(r => MapToDto(r)).ToList()
            };
        }

        public async Task UpdateResourceImageAsync(int id, string url)
        {
            Resource? resource = await unitOfWork
                .Resources
                .FindByIdAsync(id)
                ?? throw new NoSuchRecordException("Resource with given id does not exist.");
            resource.ImageUrl = url;
            unitOfWork.Resources.Update(resource);
            if (await unitOfWork.CompleteAsync() < 1)
            {
                throw new OperationFailedException("Failed to update resource image url.");
            }
        }

        public async Task<bool> ResourceExistsAsync(int id)
        {
            Resource? resource = await unitOfWork.Resources.FindByIdAsync(id);
            return resource != null;
        }

        public async Task<Resource> CreateResourceAsync(CreateResourceDto dto)
        {
            await ValidateDto(dto);
            Publisher? publisher = await unitOfWork
                .Publishers
                .FindByNameAsync(dto.Publisher)
                ?? throw new InvalidDataException("Provided publisher does not exist.");
            List<Author> authors = await unitOfWork.Authors.FindByEmailsAsync(dto.Authors);
            if (authors.Count != dto.Authors.Count)
            {
                throw new InvalidDataException("Provided authors are invalid.");
            }
            Resource resource = new Resource()
            {
                Title = dto.Title,
                Identifier = dto.Identifier,
                Descripiton = dto.Description,
                Publisher = publisher,
                PublisherId = publisher.PublisherId,
            };
            resource.Authors.AddRange(authors);
            await unitOfWork.Resources.SaveAsync(resource);
            publisher.Resources.Add(resource);
            unitOfWork.Publishers.Update(publisher);
            foreach (var author in authors)
            {
                author.Resources.Add(resource);
            }
            unitOfWork.Authors.UpdateRange(authors);
            if (await unitOfWork.CompleteAsync() < 2 + authors.Count)
            {
                throw new OperationFailedException("Failed to persist data.");
            }
            return resource;
        }

        public async Task<List<AdminResourceDto>> GetAllAdminAsync()
        {
            List<Resource> resources = await unitOfWork.Resources.FindAllWithDataAsync("ResourceId", false);
            return resources
                .Select(r => new AdminResourceDto()
                {
                    Id = r.ResourceId,
                    Title = r.Title,
                    Identifier = r.Identifier,
                    ImageUrl = r.ImageUrl,
                    Publisher = r.Publisher.Name,
                    Authors = r.Authors.Select(r => r.Email).ToList()
                }).ToList();
        }

        public async Task UpdateResourceAsync(UpdateResourceDto dto)
        {
            Resource? resource = await unitOfWork
                .Resources
                .FindByResourceIdAsync(dto.Id)
                ?? throw new NoSuchRecordException("Resource with given Id does not exist.");
            if (dto.Title.IsNullOrEmpty() || dto.Identifier.IsNullOrEmpty() || dto.Publisher.IsNullOrEmpty()) 
            {
                throw new InvalidRequestDataException("Not all required fields provided.");
            }
            Resource? resourceTitle = await unitOfWork
                .Resources
                .FindByTitleAsync(dto.Title);
            if (resourceTitle != null && resourceTitle.ResourceId.Equals(resource.ResourceId) == false)
            {
                throw new InvalidDataException("Title is already used by other resource.");
            }
            Resource? resourceIdentifier = await unitOfWork
                .Resources
                .FindByIdentifierAsync(dto.Identifier);
            if (resourceIdentifier != null && resourceIdentifier.ResourceId.Equals(resource.ResourceId) == false)
            {
                throw new InvalidDataException("Identifier is already used by other resource.");
            }
            Publisher? publisher = await unitOfWork
                .Publishers
                .FindByNameAsync(dto.Publisher)
                ?? throw new InvalidDataException("Publisher does not exist.");
            List<Author> dtoAuthors = await unitOfWork
                .Authors
                .FindByEmailsAsync(dto.Authors);
            if (dto.Authors.IsNullOrEmpty() || dtoAuthors.Count != dto.Authors.Count)
            {
                throw new InvalidDataException("Provided authors are invalid.");
            }
            if (!resource.Publisher.Name.Equals(publisher.Name)) 
            {
                Publisher old = resource.Publisher;
                resource.Publisher.Resources.Remove(resource);
                old.Resources.Remove(resource);
                publisher.Resources.Add(resource);
                resource.Publisher = publisher;
                resource.PublisherId = publisher.PublisherId;
                unitOfWork.Publishers.UpdateRange([old, publisher]);
                unitOfWork.Resources.Update(resource);
            }
            List<Author> authors = resource.Authors;
            List<Author> authorsToAdd = dtoAuthors.Except(authors).ToList();
            List<Author> authorsToRemove = authors.Except(dtoAuthors).ToList();
            resource.Authors.RemoveAll(resource => authorsToRemove.Contains(resource));
            resource.Authors.AddRange(authorsToAdd);
            foreach (Author author in authorsToRemove)
            { resource.Authors.Remove(author); }
            foreach(Author author in authorsToAdd)
            {  resource.Authors.Add(author); }
            unitOfWork.Authors.UpdateRange([.. authorsToAdd, .. authorsToRemove]);
            resource.Title = dto.Title;
            resource.Identifier = dto.Identifier;
            resource.ImageUrl = dto.ImageUrl;
            resource.Descripiton = dto.Description;
            unitOfWork.Resources.Update(resource);
            await unitOfWork.CompleteAsync();
        }

        private static ResourceDto MapToDto(Resource resource)
        {
            string publisher = resource.Publisher == null ? " " : resource.Publisher.Name;
            List<AuthorDto> authors = resource
                .Authors
                .Select(a => new AuthorDto()
                {
                    FirstName = a.FirstName,
                    LastName = a.LastName
                })
                .ToList();
            return new ResourceDto()
            {
                Id = resource.ResourceId,
                Title = resource.Title,
                Identifier = resource.Identifier,
                ImageUrl = resource.ImageUrl,
                Publisher = publisher,
                Authors = authors
            };
        }

        private async Task ValidateDto(CreateResourceDto dto)
        {
            if (dto.Title.IsNullOrEmpty() || dto.Identifier.IsNullOrEmpty())
            {
                throw new InvalidDataException("Not all required fields are provided.");
            }
            if (dto.Publisher.IsNullOrEmpty())
            {
                throw new InvalidDataException("Publisher not provided.");
            }
            if (dto.Title.Length > 100 || dto.Identifier.Length > 20)
            {
                throw new InvalidDataException("Invalid required fields length. Title: 100, identifier: 20");
            }
            if (await unitOfWork.Resources.FindByTitleAsync(dto.Title) != null)
            {
                throw new InvalidDataException("Resource with this title already exists");
            }
            if (await unitOfWork.Resources.FindByIdentifierAsync(dto.Identifier) != null)
            {
                throw new InvalidDataException("Resource with this identifier already exists");
            }
        }
    }
}
