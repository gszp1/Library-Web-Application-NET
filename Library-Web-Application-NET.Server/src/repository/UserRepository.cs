using Library_Web_Application_NET.Server.src.auth;
using Library_Web_Application_NET.Server.src.data.context;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.repository.interfaces;
using Library_Web_Application_NET.Server.src.statistics;
using Library_Web_Application_NET.Server.src.util;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library_Web_Application_NET.Server.src.repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        
        public UserRepository(LibraryDbContext context) : base(context)
        {
        }

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
            var userRole = await context.Roles.FirstOrDefaultAsync(r => r.Name.Equals("User"));

            if (userRole == null)
            {
                return 0;
            }

            return await context.UserRoles
                .Where(ur => ur.RoleId == userRole.Id)
                .CountAsync();
        }

        public async Task<long> CountByRegistrationDateMonthAsync(int month)
        {
            var userRole = await context.Roles.FirstOrDefaultAsync(r => r.Name.Equals("User"));
            
            if (userRole == null)
            {
                return 0;
            }
            
            return await context.Users
                .Where
                (
                    u => u.JoinDate.Month == month && 
                    context.UserRoles.Any(ur => ur.UserId == u.Id && ur.RoleId == userRole.Id)    
                ).CountAsync();
        }

        public async Task<List<MonthCount>> GetNumberOfRegistrationsPerMonthAsync()
        {
            return await context
                .Users
                .GroupBy(u => u.JoinDate.Month)
                .Select(u => new MonthCount() { Month = u.Key, Count = u.Count() })
                .OrderByDescending(m => m.Month)
                .ToListAsync();
        }
    }
}
