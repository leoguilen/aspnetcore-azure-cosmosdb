using LibraryApi.Repositories;
using LibraryApi.Settings;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;

namespace LibraryApi.Extensions
{
    public static class CosmosDbExtensions
    {
        public static async Task<BookRepository> InitializeCosmosClientInstanceAsync(CosmosDbSettings cosmosDbSettings)
        {
            string databaseName = cosmosDbSettings.DatabaseName;
            string containerName = cosmosDbSettings.ContainerName;
            string account = cosmosDbSettings.Account;
            string key = cosmosDbSettings.Key;

            CosmosClient client = new CosmosClient(account, key);
            BookRepository bookRepository = new BookRepository(client, cosmosDbSettings);
            DatabaseResponse database = await client
                .CreateDatabaseIfNotExistsAsync(databaseName)
                .ConfigureAwait(false);

            await database.Database
                .CreateContainerIfNotExistsAsync(containerName, "/bookId")
                .ConfigureAwait(false);

            return bookRepository;
        }
    }
}