using Library_Web_Application_NET.Server.src.data.context;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Library_Web_Application_NET.Server.src.util;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq.Expressions;

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

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }

        public async Task<List<T>> FindAllSortedAsync(Sort sort)
        {
            var query = context
                .Set<T>()
                .AsQueryable();

            if (!string.IsNullOrEmpty(sort.Field))
            {
                var param = Expression.Parameter(typeof(T), "r");
                var property = typeof(T).GetProperty(sort.Field);
                if (property != null)
                {
                    var propertyAccess = Expression.MakeMemberAccess(param, property);
                    var sortExpression = Expression.Lambda(propertyAccess, param);

                    var methodName = sort.Descending ? "OrderByDescending" : "OrderBy";

                    var resultExpression = Expression.Call(
                        typeof(Queryable),
                        methodName,
                        new Type[] { typeof(T), property.PropertyType },
                        query.Expression,
                        sortExpression
                    );
                    query = query.Provider.CreateQuery<T>(resultExpression);
                }
            }
            return await query.ToListAsync();
        }
    }
}
