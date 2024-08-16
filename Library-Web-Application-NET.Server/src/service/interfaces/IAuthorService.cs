using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.model;

namespace Library_Web_Application_NET.Server.src.service.interfaces
{
    public interface IAuthorService : IGenericService<Author>
    {
        Task CreateAuthorAsync(AdminAuthorDto dto);

        Task<List<FullAuthorDto>> GetAllAuthorsAsync();

        Task UpdateAuthorAsync(FullAuthorDto dto);
    }
}
