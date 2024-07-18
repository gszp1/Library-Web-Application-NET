using Library_Web_Application_NET.Server.src.data.context;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.repository.interfaces;

namespace Library_Web_Application_NET.Server.src.repository
{
    public class ResourceInstanceRepository : GenericRepository<ResourceInstance>, IResourceInstanceRepository
    {
        public ResourceInstanceRepository(LibraryDbContext context) : base(context)
        {
        }
    }
}
