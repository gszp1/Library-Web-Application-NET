using Library_Web_Application_NET.Server.src.model;

namespace Library_Web_Application_NET.Server.src.repository.interfaces
{
    public interface IResourceInstanceRepository : IGenericRepository<ResourceInstance>
    {
        Task<ResourceInstance> FindByResourceIdAndIsReservedFalseAsync(int resourceId);

        Task<ResourceInstance> FindByResourceIdAsync(int resourceId, string sortOrder);

        Task<List<ResourceInstance>> FindByResourceIdWithData(int resourceId, string sortOrder);

        Task<ResourceInstance> FindByIdWithData(int id);
    }
}
