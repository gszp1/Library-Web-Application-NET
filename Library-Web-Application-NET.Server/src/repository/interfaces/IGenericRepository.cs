using Library_Web_Application_NET.Server.src.data.context;
using Library_Web_Application_NET.Server.src.util;

namespace Library_Web_Application_NET.Server.src.repository.interfaces
{
    public interface IGenericRepository<T> where T : class
    {

        Task<T?> FindByIdAsync(int id);

        Task<IEnumerable<T>> FindAllAsync();

        Task SaveAsync(T entity);

        Task SaveAllAsync(IEnumerable<T> entities);

        void Update(T entity);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

        Task<List<T>> FindAllSortedAsync(Sort sort);
    }
}
