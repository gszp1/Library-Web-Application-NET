using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Library_Web_Application_NET.Server.src.service.interfaces;

namespace Library_Web_Application_NET.Server.src.service
{
    public class ResourceInstanceService : GenericService<ResourceInstance>, IResourceInstanceService 
    {
        public ResourceInstanceService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
    }
}
