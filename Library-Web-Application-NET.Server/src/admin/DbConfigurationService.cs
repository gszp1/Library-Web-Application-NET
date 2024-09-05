using Library_Web_Application_NET.Server.src.data.context;

namespace Library_Web_Application_NET.Server.src.admin
{
    public class DbConfigurationService : IDbConfigurationService
    {

        private readonly LibraryDbContext context;

        private readonly IServiceProvider serviceProvider;

        private readonly DbInitializer initializer;

        private readonly IWebHostEnvironment env;

        public DbConfigurationService(LibraryDbContext context, IServiceProvider serviceProvider, IWebHostEnvironment env) 
        {
            this.context = context;
            this.serviceProvider = serviceProvider;
            initializer = new DbInitializer(context);
            this.env = env;
        }

        public async Task CreateDatabaseWithExampleDataAsync()
        {
            //await context.Database.EnsureDeletedAsync();
            //await context.Database.EnsureCreatedAsync();
            //await initializer.SeedAdminIntoDatabase(serviceProvider);
            throw new NotImplementedException();
        }

        public async Task CreateEmptyDatabaseAsync()
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
            ClearWwwRoot();
            await initializer.SeedAdminIntoDatabase(serviceProvider);
        }

        private void ClearWwwRoot()
        {
            string imageDirPath = Path.Combine(env.WebRootPath, "images");
            string userImageDirPath = Path.Combine(env.WebRootPath, "userImages");

            if (Directory.Exists(imageDirPath))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(imageDirPath);
                foreach (FileInfo fileInfo in directoryInfo.GetFiles())
                {
                    fileInfo.Delete();
                }
            }

            if (Directory.Exists(userImageDirPath))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(userImageDirPath);
                foreach (FileInfo fileInfo in directoryInfo.GetFiles())
                {
                    fileInfo.Delete();
                }
            }

        }
    }
}
