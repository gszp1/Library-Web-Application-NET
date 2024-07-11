using Library_Web_Application_NET.Server.src.data.config;
using Library_Web_Application_NET.Server.src.model;
using Microsoft.EntityFrameworkCore;

namespace Library_Web_Application_NET.Server.src.data.context
{
    public class LibraryDbContext : DbContext
    {
        // Tables
        public DbSet<Author> Authors { get; set; }

        // Configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Entity configuration
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new PublisherConifguration());
            modelBuilder.ApplyConfiguration(new ResourceConfiguration());
            modelBuilder.ApplyConfiguration(new ResourceInstanceConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());

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

            //// Author(One) <---> AuthorResources(Many)
            //modelBuilder.Entity<Author>()
            //    .HasMany(a => a.AuthorResources)
            //    .WithOne(ar => ar.Author)
            //    .HasForeignKey(ar => ar.AuthorId)
            //    .IsRequired(true);

            //// Resource(One) <---> AuthorResources(Many)
            //modelBuilder.Entity<Resource>()
            //    .HasMany(r => r.AuthorResources)
            //    .WithOne(ar => ar.Resource)
            //    .HasForeignKey(ar => ar.ResourceId)
            //    .IsRequired(true);

            // Authors(Many) <---> Resources(Many)
            modelBuilder.Entity<Resource>()
                .HasMany(r => r.Authors)
                .WithMany(a => a.Resources)
                .UsingEntity<AuthorResource>(
                    a => a.HasOne<Author>().WithMany().HasForeignKey(a => a.AuthorId),
                    r => r.HasOne<Resource>().WithMany().HasForeignKey(r => r.ResourceId)
                );
        }
    }
}
