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

        public void Run()
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
    }
}
