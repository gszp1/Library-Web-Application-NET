using Library_Web_Application_NET.Server.src.data.context;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_Web_Application_NET.Server.src.repository
{
    public class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(LibraryDbContext context) : base(context)
        {
        }

        public Task<Publisher?> FindByNameAsync(string name)
        {
            return context.Publishers.FirstOrDefaultAsync(p => p.Name.Equals(name));
        }

        public Task<Publisher?> FindByAddressAsync(string address)
        {
            return context.Publishers.FirstOrDefaultAsync(p => p.Address.Equals(address));
        }

    }
}
