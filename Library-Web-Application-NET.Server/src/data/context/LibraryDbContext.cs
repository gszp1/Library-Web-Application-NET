using Library_Web_Application_NET.Server.src.auth.data;
using Library_Web_Application_NET.Server.src.data.config;
using Library_Web_Application_NET.Server.src.model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Library_Web_Application_NET.Server.src.data.context
{
    public class LibraryDbContext : IdentityDbContext<User, UserRole, int>
    {

        private readonly IWebHostEnvironment env;

        // Tables
        public DbSet<Author> Authors { get; set; }

        public DbSet<AuthorResource> AuthorResources { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<ResourceInstance> ResourcesInstances { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options, IWebHostEnvironment env) : base(options)
        {
            this.env = env;
        }

        // Configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Entity configuration
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new PublisherConifguration());
            modelBuilder.ApplyConfiguration(new ResourceConfiguration());
            modelBuilder.ApplyConfiguration(new ResourceInstanceConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorResourceConfiguration());

            // Define relationships

            // User(One) <---> Reservations(Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Reservations)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .IsRequired(true);

            // Instance(One) <---> Reservations(Many)
            modelBuilder.Entity<ResourceInstance>()
                .HasMany(i => i.Reservations)
                .WithOne(r => r.Instance)
                .HasForeignKey(r => r.InstanceId)
                .IsRequired(true);

            // Resource(One) <---> Instances(Many)
            modelBuilder.Entity<Resource>()
                .HasMany(r => r.Instances)
                .WithOne(i => i.Resource)
                .HasForeignKey(i => i.ResourceId)
                .IsRequired(true);

            // Publisher(One) <---> Resources(Many)
            modelBuilder.Entity<Publisher>()
                .HasMany(p => p.Resources)
                .WithOne(r => r.Publisher)
                .HasForeignKey(r => r.PublisherId)
                .IsRequired(true);

            // Authors(Many) <---> Resources(Many)
            modelBuilder.Entity<Resource>()
                .HasMany(r => r.Authors)
                .WithMany(a => a.Resources)
                .UsingEntity<AuthorResource>(
                    a => a.HasOne<Author>().WithMany().HasForeignKey(a => a.AuthorId),
                    r => r.HasOne<Resource>().WithMany().HasForeignKey(r => r.ResourceId)
                );
            
            
            List<UserRole> roles = [];
            int counter = 1;
            foreach (var roleName in Enum.GetNames(typeof(Role)))
            {
                roles.Add(new UserRole()
                {
                    Id = counter++,
                    Name = roleName,
                    NormalizedName = roleName.ToUpper()
                });
            };

            var roleClaims = new List<IdentityRoleClaim<int>>();

            int claimCounter = 1;
            foreach (var role in roles)
            {
                var permissions = RoleHandler.GetPermissions(role.Name);
                foreach (var permission in permissions)
                {
                    roleClaims.Add(new IdentityRoleClaim<int>()
                    {
                        Id = claimCounter++,
                        RoleId = role.Id,
                        ClaimType = "Permission",
                        ClaimValue = permission.ToString()
                    });
                }
            }

            modelBuilder.Entity<UserRole>().HasData(roles);
            modelBuilder.Entity<IdentityRoleClaim<int>>().HasData(roleClaims);
        }
    }
}
