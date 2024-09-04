using Library_Web_Application_NET.Server.src.data.context;

namespace Library_Web_Application_NET.Server.src.admin
{
    public class DbConfigurationService : IDbConfigurationService
    {

        private readonly LibraryDbContext context;

        private readonly IServiceProvider serviceProvider;

        private readonly DbInitializer initializer;

        public DbConfigurationService(LibraryDbContext context, IServiceProvider serviceProvider) 
        {
            this.context = context;
            this.serviceProvider = serviceProvider;
            initializer = new DbInitializer(context);
        }

        public async Task CreateDatabaseWithExampleData()
        {
            //await context.Database.EnsureDeletedAsync();
            //await context.Database.EnsureCreatedAsync();
            //await initializer.SeedAdminIntoDatabase(serviceProvider);
            throw new NotImplementedException();
        }

        public async Task CreateEmptyDatabase()
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
            await initializer.SeedAdminIntoDatabase(serviceProvider);
        }
    }
}
