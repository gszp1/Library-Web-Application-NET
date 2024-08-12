using Library_Web_Application_NET.Server.src.model;

namespace Library_Web_Application_NET.Server.src.repository.interfaces
{
    public interface IResourceRepository : IGenericRepository<Resource>
    {
        Task<List<Resource>> FindAllWithAuthorsAsync();

        // FindAllWithAuthorsPageableAsync

        Task<Resource> FindByResourceIdAsync(int resourceId);

        Task<List<Resource>> FindAllByTitleKeywordAsync(string keyword);

        // FindAllByTitleKeywordPageableAsync

        Task<Resource> FindByTitleAsync(string title);

        Task<Resource> FindByIdentifierAsync(string identifier);

        Task<List<Resource>> FinAllWithDataAsync(string sortOrder);
    }
}
