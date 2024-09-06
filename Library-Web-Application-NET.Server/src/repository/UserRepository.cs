using Library_Web_Application_NET.Server.src.auth;
using Library_Web_Application_NET.Server.src.data.context;
using Library_Web_Application_NET.Server.src.dto;
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
                .Where(u => u.Email.ToLower().IndexOf(keyword.ToLower()) >= 0)
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
            List<UserWithRole> userWithRoles = await FindAllUsersWithRolesAsync();
            return userWithRoles
                .Where(uwr => uwr.RoleName.Equals("User"))
                .GroupBy(uwr => uwr.User.JoinDate.Month)
                .Select(uwr => new MonthCount() { Month = uwr.Key, Count = uwr.Count() })
                .OrderByDescending(m => m.Month)
                .ToList();
        }

        public async Task<List<UserWithRole>> FindUsersAndRolesByEmailKeywordAsync(string keyword)
        {
            return await (from user in context.Users
                          where user.Email.ToLower().Contains(keyword.ToLower())
                          join userRole in context.UserRoles on user.Id equals userRole.UserId
                          join role in context.Roles on userRole.RoleId equals role.Id
                          select new UserWithRole { User = user, RoleName = role.Name}
                          ).ToListAsync();
        }

        public async Task<List<UserWithRole>> FindAllUsersWithRolesAsync()
        {
            return await context.Users
                .SelectMany(u => context.UserRoles.Where(ur => ur.UserId == u.Id)
                    .Join(context.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => new UserWithRole{ User = u, RoleName = r.Name }))
                .ToListAsync();
        }
    }
}
