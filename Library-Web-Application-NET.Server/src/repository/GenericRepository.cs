using Library_Web_Application_NET.Server.src.data.context;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Library_Web_Application_NET.Server.src.repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly LibraryDbContext context;

        public GenericRepository(LibraryDbContext context)
        {
            this.context = context;
        }

        public async Task SaveAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        public async Task SaveAllAsync(IEnumerable<T> entities)
        {
            await context.Set<T>().AddRangeAsync(entities);
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T?> FindByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            context.Set<T>().RemoveRange(entities);
        }
    }
}
