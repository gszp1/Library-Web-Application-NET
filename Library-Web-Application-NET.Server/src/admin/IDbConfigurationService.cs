namespace Library_Web_Application_NET.Server.src.admin
{
    public interface IDbConfigurationService
    {
        public Task CreateEmptyDatabaseAsync();

        public Task CreateDatabaseWithExampleDataAsync();
    }
}
