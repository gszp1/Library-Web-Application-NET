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

        public async Task<Publisher?> FindByNameAsync(string name)
        {
            return await context.Publishers.FirstOrDefaultAsync(p => p.Name.Equals(name));
        }

        public async Task<Publisher?> FindByAddressAsync(string address)
        {
            return await context.Publishers.FirstOrDefaultAsync(p => p.Address.Equals(address));
        }

    }
}
