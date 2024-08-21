using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.exception;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Library_Web_Application_NET.Server.src.service.interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace Library_Web_Application_NET.Server.src.service
{
    public class AuthorService : GenericService<Author>, IAuthorService
    {

        public AuthorService(IUnitOfWork unitOfWork) : base(unitOfWork) {}

        public async Task CreateAuthorAsync(AdminAuthorDto dto)
        {
            if (dto.FirstName.IsNullOrEmpty() || dto.LastName.IsNullOrEmpty() || dto.Email.IsNullOrEmpty())
            {
                throw new InvalidRequestDataException("Not all request data provided.");
            }
            if (dto.FirstName.Length > 40 || dto.LastName.Length > 40 || dto.Email.Length > 50)
            {
                throw new InvalidRequestDataException("Data fields have incorrect lengths. (first name 0-40, last name 0-40, email 0-50.");
            }
            if (ValidateEmail(dto.Email) == false)
            {
                throw new InvalidRequestDataException("Invalid email address.");
            }
            Author? author = await unitOfWork.Authors.FindByEmailAsync(dto.Email);
            if (author != null)
            {
                throw new OperationNotAvailableException("Author with given email already exists.");
            }
            Author newAuthor = new Author
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };
            await unitOfWork.Authors.SaveAsync(newAuthor);
            if (await unitOfWork.CompleteAsync() == 0)
            {
                throw new OperationFailedException("Failed to persist author in database.");
            }
        }

        public async Task<List<FullAuthorDto>> GetAllAuthorsAsync()
        {
            List<Author> authors = await unitOfWork.Authors.FindAllSortedAsync("AuthorId", false);
            return authors
                .Select(a => new FullAuthorDto
                {
                    AuthorId = a.AuthorId,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Email = a.Email
                })
                .ToList();
        }

        public async Task UpdateAuthorAsync(FullAuthorDto dto)
        {
            if (dto.AuthorId == null || dto.FirstName.IsNullOrEmpty() || dto.LastName.IsNullOrEmpty() || dto.Email.IsNullOrEmpty())
            {
                throw new InvalidRequestDataException("Not all required data provided.");
            }
            if (dto.FirstName.Length > 40 || dto.LastName.Length > 40 || dto.Email.Length > 50)
            {
                throw new InvalidRequestDataException("Data fields have incorrect lengths. (first name 0-40, last name 0-40, email 0-50.");
            }
            if (ValidateEmail(dto.Email) == false)
            {
                throw new InvalidRequestDataException("Invalid email address.");
            }
            Author? emailAuthor = await unitOfWork.Authors.FindByEmailAsync(dto.Email);
            if (emailAuthor != null)
            {
                throw new OperationNotAvailableException("Author with given email already exists.");
            }
            int authorId = dto.AuthorId ?? 0;
            Author? author = await unitOfWork.Authors.FindByIdAsync(authorId)
                ?? throw new NoSuchRecordException("Author with given id does not exist.");
            author.Email = dto.Email;
            author.FirstName = dto.FirstName;
            author.LastName = dto.LastName;
            unitOfWork.Authors.Update(author);
            if (await unitOfWork.CompleteAsync() == 0)
            {
                throw new OperationFailedException("Failed to persist changes to author in database.");
            }
        }

        private static bool ValidateEmail(string email)
        {
            string pattern = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);
            return regex.Match(email).Success;
        }
    }
}
