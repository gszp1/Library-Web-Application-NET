using Library_Web_Application_NET.Server.src.auth;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.util;
using Microsoft.AspNetCore.Identity;

namespace Library_Web_Application_NET.Server.src.data.context
{
    public class DbInitializer
    {
        private readonly LibraryDbContext context;

        public DbInitializer(LibraryDbContext context)
        {
            this.context = context;
        }

        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<UserRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            IdentityResult roleResult;

            foreach (var roleName in Enum.GetNames(typeof(Role)))
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    roleResult = await roleManager.CreateAsync(new UserRole
                    { 
                        Name = roleName,
                        NormalizedName = roleName.ToUpper() 
                    });    
                }
            }

            var adminUser = new User
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                JoinDate = DateOnly.FromDateTime(DateTime.Now),
                Status = UserStatus.Active,
                EmailConfirmed = true
            };

            string password = "AdminAdmin1";
            var user = await userManager.FindByEmailAsync(adminUser.Email);
            if (user == null)
            {
                var createAdminResult = await userManager.CreateAsync(adminUser, password);
                if (createAdminResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }

        public void Run(IServiceProvider serviceProvider)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            Initialize(serviceProvider).Wait();
        }
    }
}
