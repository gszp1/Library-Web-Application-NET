using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.exception;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Library_Web_Application_NET.Server.src.service.interfaces;
using Microsoft.IdentityModel.Tokens;
using Library_Web_Application_NET.Server.src.util;

namespace Library_Web_Application_NET.Server.src.service
{
    public class PublisherService : GenericService<Publisher>, IPublisherService
    {
        public PublisherService(IUnitOfWork unitOfWork) : base(unitOfWork) {}

        public async Task CreatePublisherAsync(PublisherDto dto)
        {
            if (dto.Address.IsNullOrEmpty() || dto.Name.IsNullOrEmpty())
            {
                throw new InvalidRequestDataException("Not all requested fields are provided.");
            }
            if (dto.Name.Length > 100 || dto.Address.Length > 150)
            {
                throw new InvalidRequestDataException("Provided fields have invalid size (name 0-100, address 0-150).");
            }
            if (await unitOfWork.Publishers.FindByNameAsync(dto.Name) != null)
            {
                throw new OperationNotAvailableException("Publisher with given name already exists.");
            }
            if (await unitOfWork.Publishers.FindByAddressAsync(dto.Address) != null)
            {
                throw new OperationNotAvailableException("Publisher with given address already exists.");
            }
            Publisher publisher = new Publisher
            {
                Address = dto.Address,
                Name = dto.Name
            };
            await unitOfWork.Publishers.SaveAsync(publisher);
            if (await unitOfWork.CompleteAsync() < 1)
            {
                throw new OperationFailedException("Failed to persist publisher to database.");
            }
        }

        public async Task<List<AdminPublisherDto>> GetAllPublishersAsync()
        {
            List<Publisher> publishers = await unitOfWork
                .Publishers
                .FindAllSortedAsync(new Sort("PublisherId", false));
            return publishers.Select(p => new AdminPublisherDto
            {
                Name = p.Name,
                Address = p.Address,
                PublisherId = p.PublisherId,
            })
            .ToList();
        }

        public async Task UpdatePublisherAsync(AdminPublisherDto dto)
        {
            if (dto.Address.IsNullOrEmpty() || dto.Name.IsNullOrEmpty() || dto.PublisherId == null)
            {
                throw new InvalidRequestDataException("Not all requested fields are provided.");
            }
            if (dto.Name.Length > 100 || dto.Address.Length > 150)
            {
                throw new InvalidRequestDataException("Provided fields have invalid size (name 0-100, address 0-150).");
            }
            int publisherId = dto.PublisherId ?? 0;
            Publisher? publisher = await unitOfWork.Publishers.FindByIdAsync(publisherId);
            if (publisher == null)
            {
                throw new NoSuchRecordException("Publisher with given id does not exist.");
            }
            Publisher? namePublisher = await unitOfWork.Publishers.FindByNameAsync(dto.Name);
            if (namePublisher != null && namePublisher.PublisherId != publisherId)
            {
                throw new OperationNotAvailableException("Publisher with given name already exists.");
            }

            Publisher? addressPublisher = await unitOfWork.Publishers.FindByAddressAsync(dto.Address);
            if (addressPublisher != null && addressPublisher.PublisherId != publisherId)
            {
                throw new OperationNotAvailableException("Publisher with given address already exists.");
            }
            publisher.Address = dto.Address;
            publisher.Name = dto.Name;
            unitOfWork.Publishers.Update(publisher);
            if (await unitOfWork.CompleteAsync() < 1)
            {
                throw new OperationFailedException("Failed to persist publisher update to database.");
            }
        }
    }
}
