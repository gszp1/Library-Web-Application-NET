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
        }
    }
}
