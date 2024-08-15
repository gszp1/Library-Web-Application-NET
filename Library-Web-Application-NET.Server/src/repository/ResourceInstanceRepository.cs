using Library_Web_Application_NET.Server.src.data.context;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library_Web_Application_NET.Server.src.repository
{
    public class ResourceInstanceRepository : GenericRepository<ResourceInstance>, IResourceInstanceRepository
    {
        public ResourceInstanceRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task<List<ResourceInstance>> FindByResourceIdAndIsReservedFalseAsync(int resourceId)
        {
            return await context
                .ResourcesInstances
                .Where(ri => ri.Reserved == false && ri.Resource.ResourceId == resourceId)
                .ToListAsync();
        }

        public async Task<List<ResourceInstance>> FindByResourceIdAsync(int resourceId, string sortBy, bool descending)
        {
            var query = context
                .ResourcesInstances
                .Where(r => r.Resource.ResourceId == resourceId)
                .AsQueryable();
            if (!string.IsNullOrEmpty(sortBy))
            {
                var param = Expression.Parameter(typeof(ResourceInstance), "r");
                var property = typeof(ResourceInstance).GetProperty(sortBy);
                if (property != null)
                {
                    var sortExpression = Expression.Lambda(Expression.Property(param, property), param);

                    var methodName = descending ? "OrderByDescending" : "OrderBy";

                    var resultExpression = Expression.Call(
                        typeof(Queryable),
                        methodName,
                        new Type[] { typeof(ResourceInstance), property.PropertyType },
                        query.Expression,
                        sortExpression);
                    query = query.Provider.CreateQuery<ResourceInstance>(resultExpression);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<List<ResourceInstance>> FindByResourceIdWithData(int resourceId, string sortBy, bool descending)
        {
            var query = context
                .ResourcesInstances
                .Where(ri => ri.Resource.ResourceId == resourceId)
                .Include(ri => ri.Resource)
                .Include(ri => ri.Reservations)
                .AsQueryable();
            if (!string.IsNullOrEmpty(sortBy))
            {
                var param = Expression.Parameter(typeof(ResourceInstance), "r");
                var property = typeof(ResourceInstance).GetProperty(sortBy);
                if (property != null)
                {
                    var sortExpression = Expression.Lambda(Expression.Property(param, property), param);

                    var methodName = descending ? "OrderByDescending" : "OrderBy";

                    var resultExpression = Expression.Call(
                        typeof(Queryable),
                        methodName,
                        new Type[] { typeof(ResourceInstance), property.PropertyType },
                        query.Expression,
                        sortExpression);
                    query = query.Provider.CreateQuery<ResourceInstance>(resultExpression);
                }
            }
            return await query.ToListAsync();
        }
            
        public async Task<ResourceInstance?> FindByIdWithData(int id)
        {
            return await context
                .ResourcesInstances
                .Include(ri => ri.Resource)
                .Include(ri => ri.Reservations)
                .FirstOrDefaultAsync(ri => ri.InstanceId == id);
        }
    }
}
