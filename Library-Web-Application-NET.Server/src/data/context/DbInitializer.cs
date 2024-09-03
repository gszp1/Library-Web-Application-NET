using Library_Web_Application_NET.Server.src.auth;
using Library_Web_Application_NET.Server.src.auth.data;
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

        public void Run(IServiceProvider serviceProvider)
        {
            context.Database.EnsureCreated();
        }

        public async void SeedAdminIntoDatabase(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<UserRole>>();

                var roleName = "Admin";
                if (!await roleManager.RoleExistsAsync(roleName)) {
                    var adminRole = new UserRole() { Name = roleName , NormalizedName = roleName.ToUpper()};
                    await roleManager.CreateAsync(adminRole);
                    var roleClaims = new List<IdentityRoleClaim<int>>();
                    foreach (var permission in RoleHandler.GetPermissions(roleName))
                    {
                        roleClaims.Add(new IdentityRoleClaim<int>()
                        {
                            RoleId = adminRole.Id,
                            ClaimType = "Permission",
                            ClaimValue = permission.ToString()
                        });
                        
                    }
                    await context.RoleClaims.AddRangeAsync(roleClaims);
                }
                await context.SaveChangesAsync();

                var adminEmail = "Admin@Admin.com";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    adminUser = new User()
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        JoinDate = DateOnly.FromDateTime(DateTime.Now),
                        Status = UserStatus.Active,
                        PhoneNumberConfirmed = true,
                        LockoutEnabled = false,
                        EmailConfirmed = true
                    };
                    var password = "AdminAdmin1";
                    var result = await userManager.CreateAsync(adminUser, password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(adminUser, roleName);
                    } 
                    else
                    {
                        Console.WriteLine("WARNING! No admin user exists - failed to seed admin user into database\n Errors:\n");
                        foreach (var error in result.Errors)
                        {
                            Console.WriteLine($"Error: {error.Code} {error.Description}");
                        }
                    }
                }
            }
        }
    }
}
