using Library_Web_Application_NET.Server.src.model;

namespace Library_Web_Application_NET.Server.src.repository.interfaces
{
    public interface IResourceRepository : IGenericRepository<Resource>
    {
        // FindAllPageableAsync

        Task<List<Resource>> FindAllByTitleKeywordAsync(string keyword);

        Task<Resource> FindByTitleAsync(string title);

        Task<Resource> FindByIdentifierAsync(string identifier);

        Task<Resource> FindAllPageable();
    }
}
