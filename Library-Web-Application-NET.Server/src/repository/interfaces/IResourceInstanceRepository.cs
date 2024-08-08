using Library_Web_Application_NET.Server.src.model;

namespace Library_Web_Application_NET.Server.src.repository.interfaces
{
    public interface IResourceInstanceRepository : IGenericRepository<ResourceInstance>
    {
        Task<List<ResourceInstance>> FindByResourceIdAndIsReservedFalseAsync(int resourceId);

        Task<List<ResourceInstance>> FindByResourceIdAsync(int resourceId, string sort);

        Task<ResourceInstance> FindByResourceIdAsync(int resourceId);
    }
}
