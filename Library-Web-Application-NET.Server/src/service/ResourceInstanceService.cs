using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.exception;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Library_Web_Application_NET.Server.src.service.interfaces;
using Library_Web_Application_NET.Server.src.util;
using Microsoft.Identity.Client;

namespace Library_Web_Application_NET.Server.src.service
{
    public class ResourceInstanceService : GenericService<ResourceInstance>, IResourceInstanceService 
    {
        public ResourceInstanceService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<List<InstanceDto>> GetNotReservedInstancesOfResourceAsync(int resourceId)
        {
            List<ResourceInstance> instances = await unitOfWork
                .ResourceInstances
                .FindByResourceIdAndIsReservedFalseAsync(resourceId);
            return instances.Select(i => new InstanceDto
            {
                Id = i.InstanceId,
                ResourceId = i.ResourceId,
                IsReserved = i.Reserved
            })
            .ToList();
        }

        public async Task<ResourceInstance?> GetResourceInstanceByIdAsync(int instanceId)
        {
            return await unitOfWork.ResourceInstances.FindByIdAsync(instanceId);
        }

        public async Task SaveResourceInstanceAsync(ResourceInstance resourceInstance)
        {
            await unitOfWork.ResourceInstances.SaveAsync(resourceInstance);
            if (await unitOfWork.CompleteAsync() != 1)
            {
                throw new OperationFailedException("Failed to persist instance in database.");
            }
        }

        public async Task<List<InstanceDto>> GetAllInstancesByResourceIdAsync(int resourceId, string sortBy, bool descending)
        {
            List<ResourceInstance> instances = await unitOfWork
                .ResourceInstances
                .FindByResourceIdAsync(resourceId, sortBy, descending);
            return instances.Select(i => new InstanceDto
            {
                Id = i.InstanceId,
                ResourceId = i.ResourceId,
                IsReserved = i.Reserved
            })
            .ToList();
        }

        public async Task<List<AdminInstanceDto>> GetAllAdminInstancesByResourceIdAsync(int id, string sortBy, bool descending)
        {
            List<ResourceInstance> instances = await unitOfWork
                .ResourceInstances
                .FindByResourceIdWithData(id, sortBy, descending);
            return instances.Select(i => new AdminInstanceDto
            {
                ResourceId = i.ResourceId,
                Id = i.InstanceId,
                IsReserved = i.Reserved,
                InstanceStatus = i.Status
            })
            .ToList();
        }

        public async Task SaveAllAsync(List<ResourceInstance> resourceInstances)
        {
            await unitOfWork.ResourceInstances.SaveAllAsync(resourceInstances);
            if (await unitOfWork.CompleteAsync() < resourceInstances.Count)
            {
                throw new OperationFailedException("Failed to persist instances.");
            }
        }

        public async Task SaveAsync(ResourceInstance resourceInstance)
        {
            await unitOfWork.ResourceInstances.SaveAsync(resourceInstance);
            if (await unitOfWork.CompleteAsync() < 1)
            {
                throw new OperationFailedException("Failed to persist instance.");
            }
        }

        public async Task WithdrawInstanceAsync(int id)
        {
            ResourceInstance? instance = await unitOfWork
                .ResourceInstances
                .FindByIdWithData(id)
                ?? throw new NoSuchRecordException("Instance with given id does not exist.");
            if (instance.Reserved == false)
            {
                instance.Status = InstanceStatus.Withdrawn;
                unitOfWork.ResourceInstances.Update(instance);
                if (await unitOfWork.CompleteAsync() < 1)
                {
                    throw new OperationFailedException("Failed to withraw instance.");
                }
                return;
            }
            List<Reservation> activeReservations = instance
                .Reservations
                .Where(r => r.Status == ReservationStatus.Active)
                .ToList();
            List<Reservation> borrowedReservations = instance
                .Reservations
                .Where(r => r.Status == ReservationStatus.Borrowed)
                .ToList();
            if (activeReservations.Count == 1)
            {
                Reservation reservation = activeReservations.First();
                reservation.Status = ReservationStatus.Cancelled;
                instance.Status = InstanceStatus.Withdrawn;
                instance.Reserved = false;
                unitOfWork.Reservations.Update(reservation);
                unitOfWork.ResourceInstances.Update(instance);
                if (await unitOfWork.CompleteAsync() < 2)
                {
                    throw new OperationFailedException("Failed to withraw instance.");
                }
                return;
            }
            if (borrowedReservations.Count == 1)
            {
                Reservation reservation = activeReservations.First();
                instance.Status = InstanceStatus.Awaiting_withdrawal;
                unitOfWork.ResourceInstances.Update(instance);
                if (await unitOfWork.CompleteAsync() < 1)
                {
                    throw new OperationFailedException("Failed to withraw instance.");
                }
                throw new OperationNotAvailableException("Could not withdraw instance because it is borrowed.");
            }


        }

        public async Task UpdateInstanceAsync(AdminInstanceDto dto)
        {
            ResourceInstance? instance = await unitOfWork
                .ResourceInstances
                .FindByIdWithData(dto.Id)
                ?? throw new NoSuchRecordException("Instance with given id does not exist.");
            if (dto.IsReserved && dto.InstanceStatus == InstanceStatus.Withdrawn ||
               (!dto.IsReserved && dto.InstanceStatus == InstanceStatus.Awaiting_withdrawal)
            )
            {
                throw new OperationNotAvailableException("Invalid paramters.");
            }
            instance.Reserved = dto.IsReserved;
            instance.Status = dto.InstanceStatus;
            unitOfWork.ResourceInstances.Update(instance);
            if (await unitOfWork.CompleteAsync() < 1)
            {
                throw new OperationFailedException("Failed to persist changes.");
            }
            
        }

        public async Task CreateInstanceAsync(int resourceId)
        {
            Resource? resource = await unitOfWork
                .Resources
                .FindByResourceIdAsync(resourceId)
                ?? throw new NoSuchRecordException("Resource with given id does not exist.");
            ResourceInstance instance = new ResourceInstance()
            {
                Status = InstanceStatus.Active,
                Reserved = false,
                Resource = resource,
                ResourceId = resource.ResourceId
            };
            resource.Instances.Add(instance);
            await unitOfWork.ResourceInstances.SaveAsync(instance);
            unitOfWork.Resources.Update(resource);
            if (await unitOfWork.CompleteAsync() < 2)
            {
                throw new OperationFailedException ("Failed to create resource instance.");
            }
        }

        public async Task<List<InstanceDto>> GetNotReservedInstancesOfResource(int resourceId)
        {
            List<ResourceInstance> instances = await unitOfWork
                .ResourceInstances
                .FindByResourceIdAndIsReservedFalseAsync(resourceId);
            return instances
                .Select(i => new InstanceDto()
                { 
                    Id = i.InstanceId,
                    IsReserved = i.Reserved,
                    ResourceId = i.ResourceId
                }
                ).ToList();
        }
    }
}
