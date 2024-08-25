using Library_Web_Application_NET.Server.src.model;

namespace Library_Web_Application_NET.Server.src.repository.interfaces
{
    public interface IPublisherRepository : IGenericRepository<Publisher>
    {
        Task<Publisher?> FindByNameAsync(string name);

        Task<Publisher?> FindByAddressAsync(string address);
    }
}
