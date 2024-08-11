using Library_Web_Application_NET.Server.src.data.context;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Microsoft.EntityFrameworkCore;

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
    }
}
