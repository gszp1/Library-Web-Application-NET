﻿namespace Library_Web_Application_NET.Server.src.admin
{
    public interface IDbConfigurationService
    {
        public Task CreateEmptyDatabase();

        public Task CreateDatabaseWithExampleData();
    }
}
