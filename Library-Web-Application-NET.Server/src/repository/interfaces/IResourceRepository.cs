using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.util;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Library_Web_Application_NET.Server.src.repository.interfaces
{
    public interface IResourceRepository : IGenericRepository<Resource>
    {
        Task<List<Resource>> FindAllWithAuthorsAsync();

        Task<PagedResult<Resource>> FindAllWithAuthorsPageableAsync(Pageable pageable);

        Task<Resource?> FindByResourceIdAsync(int resourceId);

        Task<List<Resource>> FindAllByTitleKeywordAsync(string keyword);

        Task<PagedResult<Resource>> FindAllByTitleKeywordPageableAsync(string keyword, Pageable pageable);

        Task<Resource?> FindByTitleAsync(string title);

        Task<Resource?> FindByIdentifierAsync(string identifier);

        Task<List<Resource>> FindAllWithDataAsync(string sortBy, bool descending);
    }
}
