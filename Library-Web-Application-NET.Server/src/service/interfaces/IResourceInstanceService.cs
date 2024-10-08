﻿using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.model;

namespace Library_Web_Application_NET.Server.src.service.interfaces
{
    public interface IResourceInstanceService : IGenericService<ResourceInstance> 
    {
        Task<List<InstanceDto>> GetNotReservedInstancesOfResourceAsync(int resourceId);

        Task<ResourceInstance?> GetResourceInstanceByIdAsync(int instanceId);

        Task SaveResourceInstanceAsync(ResourceInstance resourceInstance);

        Task<List<InstanceDto>> GetAllInstancesByResourceIdAsync(int resourceId, string sortBy, bool descending);

        Task<List<AdminInstanceDto>> GetAllAdminInstancesByResourceIdAsync(int id, string sortBy, bool descending);

        Task SaveAllAsync(List<ResourceInstance> resourceInstances);

        Task SaveAsync(ResourceInstance resourceInstance);

        Task WithdrawInstanceAsync(int id);

        Task UpdateInstanceAsync(AdminInstanceDto dto);

        Task CreateInstanceAsync(int resourceId);

        Task<List<InstanceDto>> GetNotReservedInstancesOfResource(int resourceId);
    }
}
