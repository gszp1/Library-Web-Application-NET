using Library_Web_Application_NET.Server.src.data.context;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Library_Web_Application_NET.Server.src.util;
using Microsoft.EntityFrameworkCore;

namespace Library_Web_Application_NET.Server.src.repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(LibraryDbContext context) : base(context) { }

        public async Task<User?> FindByEmailAsync(string email) 
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public async Task<List<User>> FindByEmailKeywordAsync(string keyword)
        {
            return await context
                .Users
                .Where(u => u.Email.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                .ToListAsync();
        }

        public async Task<User?> FindByUserIdAsync(int userId)
        {
            return await context.Users.FindAsync(userId);
        }

        public async Task<long> CountUsersAsync()
        {
            return await context
                .Users
                .Where(u => u.Role == Role.User)
                .CountAsync();
        }

        public async Task<long> CountByRegistrationDateMonthAsync(int month)
        {
            return await context
                .Users
                .Where(u => u.Role == Role.User && u.JoinDate.Month == month)
                .CountAsync();
        }
    }
}
