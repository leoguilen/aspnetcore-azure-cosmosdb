using LibraryApi.Models;
using LibraryApi.Settings;
using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LibraryApi.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly Container _container;

        public BookRepository(CosmosClient cosmosClient,
            CosmosDbSettings cosmosDbSettings)
        {
            this._container = cosmosClient
              .GetContainer(cosmosDbSettings.DatabaseName,
                cosmosDbSettings.ContainerName);
        }

        public async Task<bool> CreateAsync(Book obj)
        {
            obj.Id = obj.BookId;
            var created = await _container
              .CreateItemAsync(obj, new PartitionKey(obj.BookId.ToString()))
              .ConfigureAwait(false);

            return created.StatusCode == HttpStatusCode.Created;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var book = await FindByIdAsync(id)
                .ConfigureAwait(false);

            if (book is null)
                return false;

            var deleted = await _container
              .DeleteItemAsync<Book>(book.BookId.ToString(), new PartitionKey(book.BookId.ToString()))
              .ConfigureAwait(false);

            return deleted.StatusCode == HttpStatusCode.NoContent;
        }

        public async Task<IEnumerable<Book>> FindAllAsync()
        {
            const string command = "SELECT * FROM Book";

            var query = this._container
              .GetItemQueryIterator<Book>(new QueryDefinition(command));
            var results = new List<Book>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync()
                    .ConfigureAwait(false);

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task<Book> FindByIdAsync(Guid id)
        {
            const string command = "SELECT * FROM Book b WHERE b.bookId = '{id}'";

            var query = this._container
              .GetItemQueryIterator<Book>(new QueryDefinition(
                  command.Replace("{id}", id.ToString())));
            var result = await query.ReadNextAsync()
                .ConfigureAwait(false);

            return result.FirstOrDefault();
        }

        public async Task<bool> UpdateAsync(Guid id, Book obj)
        {
            var bookObj = await FindByIdAsync(id)
                .ConfigureAwait(false);

            if (bookObj is null)
                return false;

            obj.Id = bookObj.Id;
            obj.BookId = bookObj.BookId;

            var updated = await _container
                .UpsertItemAsync(obj, new PartitionKey(id.ToString()))
                .ConfigureAwait(false);

            return updated.StatusCode == HttpStatusCode.OK;
        }
    }
}