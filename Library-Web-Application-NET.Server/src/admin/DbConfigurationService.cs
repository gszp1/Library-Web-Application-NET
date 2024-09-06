using Library_Web_Application_NET.Server.src.data.context;
using Library_Web_Application_NET.Server.src.exception;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
            await context.Database.EnsureDeletedAsync();
            ClearWwwRoot();

            CopyFiles
            (
                Path.Combine(env.WebRootPath, "sampleImages"),
                Path.Combine(env.WebRootPath, "images")
            );
            CopyFiles
            (
                Path.Combine(env.WebRootPath, "sampleUserImages"),
                Path.Combine(env.WebRootPath, "userImages")
            );

            await context.Database.EnsureCreatedAsync();
            string scriptPath = Path.Combine(env.WebRootPath, "sql", "LibraryDbScriptData.sql");
            string script = await File.ReadAllTextAsync(scriptPath);

            using (var connection = new SqlConnection(context.Database.GetDbConnection().ConnectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction = connection.BeginTransaction();
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    foreach (string batch in script.Split(new string[] { "GO" }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        command.CommandText = batch;
                        await command.ExecuteNonQueryAsync();
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                    transaction.Rollback();
                    throw new OperationFailedException("Failed to recreate database with data.");
                }
            }
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

        private void CopyFiles(string src, string dst)
        {
            try
            {
                if (!Directory.Exists(src))
                {
                    return;
                }
                if (!Directory.Exists(dst))
                {
                    Directory.CreateDirectory(dst);
                }
                DirectoryInfo dirInfo = new DirectoryInfo(src);
                FileInfo[] files = dirInfo.GetFiles();
                foreach (FileInfo file in files)
                {
                    string destFilePath = Path.Combine(dst, file.Name);

                    file.CopyTo(destFilePath, overwrite: true);
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
