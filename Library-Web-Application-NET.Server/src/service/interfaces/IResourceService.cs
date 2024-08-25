using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.util;

namespace Library_Web_Application_NET.Server.src.service.interfaces
{
    public interface IResourceService : IGenericService<Resource>
    {
        Task<List<ResourceDto>> GetAllWithAuthorsAsync();

        Task<PagedResult<ResourceDto>> GetAllWithAuthorsPageableAsync(Pageable pageable);

        Task<ResourceDescriptionDto> GetResourceDescriptionAsync(int id);

        Task<List<ResourceDto>> GetResourcesWithKeywordInTitleAsync(string keyword);

        Task<PagedResult<ResourceDto>> GetResourcesWithKeywordInTitlePageableAsync(string keyword, Pageable pageable);

        Task UpdateResourceImageAsync(int id, string url);

        Task<bool> ResourceExistsAsync(int id);

        Task<Resource> CreateResourceAsync(CreateResourceDto dto);

        Task<List<AdminResourceDto>> GetAllAdminAsync();

        Task UpdateResourceAsync(UpdateResourceDto dto);
    }
}
