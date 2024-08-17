using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.exception;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Library_Web_Application_NET.Server.src.service.interfaces;

namespace Library_Web_Application_NET.Server.src.service
{
    public class AuthorService : GenericService<Author>, IAuthorService
    {
        public async Task CreateAuthorAsync(AdminAuthorDto dto)
        {
            if (dto.FirstName == null || dto.LastName == null || dto.Email == null)
            {
                throw new InvalidRequestDataException("Not all needed data provided.");
            }

        }

        Task<List<FullAuthorDto>> GetAllAuthorsAsync();

        Task UpdateAuthorAsync(FullAuthorDto dto);
    }
}
