using Library_Web_Application_NET.Server.src.data.context;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library_Web_Application_NET.Server.src.repository
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryDbContext context) : base(context)
        {
        }

        public async Task<Author?> FindByEmailAsync(string email)
        {
            return await context.Authors.FirstOrDefaultAsync(a => a.Email.Equals(email));
        }

        public async Task<Author?> FindByAuthorIdAsync(int authorId)
        {
            return await context.Authors.FindAsync(authorId);
        }

        public async Task<List<Author>> FindByEmailsAsync(List<string> emails)
        {
            return await context
                .Authors
                .Where(a => emails.Contains(a.Email))
                .ToListAsync();
        }

        public async Task<List<Author>> FindAllSortedAsync(string sortBy, bool sortDescending)
        {
            var query = context
                .Authors
                .AsQueryable();

            if (!string.IsNullOrEmpty(sortBy))
            {
                var param = Expression.Parameter(typeof(Author), "r");
                var property = typeof(Author).GetProperty(sortBy);
                if (property != null)
                {
                    var sortExpression = Expression.Lambda(Expression.Property(param, property), param);

                    var methodName = sortDescending ? "OrderByDescending" : "OrderBy";

                    var resultExpression = Expression.Call(
                        typeof(Queryable),
                        methodName,
                        new Type[] { typeof(Author), property.PropertyType },
                    query.Expression,
                        sortExpression);
                    query = query.Provider.CreateQuery<Author>(resultExpression);
                }
            }
            return await query.ToListAsync();
        }
    }
}
