using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.model;

namespace Library_Web_Application_NET.Server.src.service.interfaces
{
    public interface IResourceInstanceService : IGenericService<ResourceInstance> 
    {
        Task<List<InstanceDto>> GetNotReservedInstancesOfResourceAsync(int resourceId);

        Task<ResourceInstance> GetResourceInstanceByIdAsync(int instanceId);

        Task<ResourceInstance> SaveResourceInstanceAsync(ResourceInstance resourceInstance);

    }
}
