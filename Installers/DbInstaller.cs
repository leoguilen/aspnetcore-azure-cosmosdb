using System.Diagnostics;
using LibraryApi.Extensions;
using LibraryApi.Repositories;
using LibraryApi.Services;
using LibraryApi.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LibraryApi.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var cosmosDbSettings = new CosmosDbSettings();
            cosmosDbSettings.Account = Environment.GetEnvironmentVariable("COSMOSDB_ACCOUNT");
            cosmosDbSettings.Key = Environment.GetEnvironmentVariable("COSMOSDB_KEY").AddMissingCaracters();
            cosmosDbSettings.DatabaseName = configuration.GetSection("CosmosDb").GetValue<string>("DatabaseName");
            cosmosDbSettings.ContainerName = configuration.GetSection("CosmosDb").GetValue<string>("ContainerName");
            services.AddSingleton(cosmosDbSettings);

            services.AddSingleton<IBookRepository>(CosmosDbExtensions
              .InitializeCosmosClientInstanceAsync(cosmosDbSettings)
                  .GetAwaiter()
                  .GetResult());

            services.AddTransient<IBookService, BookService>();
        }
    }
}
