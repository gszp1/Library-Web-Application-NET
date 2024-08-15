using Library_Web_Application_NET.Server.src.data.context;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Library_Web_Application_NET.Server.src.util;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library_Web_Application_NET.Server.src.repository
{
    public class ResourceRepository : GenericRepository<Resource>, IResourceRepository
    {
        public ResourceRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task<List<Resource>> FindAllWithAuthorsAsync()
        {
            return await context
                .Resources
                .Include(r => r.Publisher)
                .Include(r => r.Authors)
                .ToListAsync();
        }

        public async Task<PagedResult<Resource>> FindAllWithAuthorsPageableAsync(Pageable pageable)
        {
            var query = context
                .Resources
                .Include(r => r.Publisher)
                .Include(r => r.Authors)
                .AsQueryable();

            var totalItems = await query.CountAsync();
            
            var items = await query
                .Skip((pageable.PageNumber - 1) * pageable.PageSize)
                .Take(pageable.PageSize)
                .ToListAsync();

            return new PagedResult<Resource>
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = pageable.PageNumber,
                PageSize = pageable.PageSize
            };
        }

        public async Task<Resource?> FindByResourceIdAsync(int resourceId)
        {
            return await context
                .Resources
                .FindAsync(resourceId);
        }

        public async Task<List<Resource>> FindAllByTitleKeywordAsync(string keyword)
        {
            return await context
                .Resources
                .Where(r => r.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }

        public async Task<PagedResult<Resource>> FindAllByTitleKeywordPageableAsync(string keyword, Pageable pageable)
        {
            var query = context
                .Resources
                .Where(r => r.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .Include(r => r.Publisher)
                .Include(r => r.Authors)
                .AsQueryable();
            
            var totalItems = await query.CountAsync();

            var items = await query
                .Skip((pageable.PageNumber - 1) * pageable.PageSize)
                .Take(pageable.PageSize)
                .ToListAsync();

            return new PagedResult<Resource>
            {
                Items = items,
                TotalItems = totalItems,
                PageNumber = pageable.PageNumber,
                PageSize = pageable.PageSize
            };
        }   

        public async Task<Resource?> FindByTitleAsync(string title)
        {
            return await context
                .Resources
                .Where(r => r.Title.Equals(title))
                .FirstOrDefaultAsync();
        }

        public async Task<Resource?> FindByIdentifierAsync(string identifier)
        {
            return await context
                .Resources
                .Where(r => r.Identifier.Equals(identifier))
                .FirstOrDefaultAsync();
        }

        public async Task<List<Resource>> FinAllWithDataAsync(string sortBy, bool descending)
        {
            var query = context
                .Resources
                .Include(r => r.Publisher)
                .Include(r => r.Authors)
                .AsQueryable();
            if (!string.IsNullOrEmpty(sortBy))
            {
                var param = Expression.Parameter(typeof(Resource), "r");
                var property = typeof(Resource).GetProperty(sortBy);
                if (property != null)
                {
                    var sortExpression = Expression.Lambda(Expression.Property(param, property), param);

                    var methodName = descending ? "OrderByDescending" : "OrderBy";

                    var resultExpression = Expression.Call(
                        typeof(Queryable),
                        methodName,
                        new Type[] {typeof(Resource), property.PropertyType},
                        query.Expression,
                        sortExpression);
                    query = query.Provider.CreateQuery<Resource>(resultExpression);
                }
            } 
            return await query.ToListAsync();
        }

    }
}
