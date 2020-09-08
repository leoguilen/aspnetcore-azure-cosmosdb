using LibraryApi.Models;
using LibraryApi.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BookService> _logger;

        public BookService(IBookRepository bookRepository,
            ILogger<BookService> logger)
        {
            this._bookRepository = bookRepository;
            _logger = logger;
        }

        public async Task<bool> AddBookAsync(Book book)
        {
            return await _bookRepository
              .CreateAsync(book);
        }

        public async Task<bool> DeleteBookAsync(Guid id)
        {
            return await _bookRepository
              .DeleteAsync(id);
        }

        public async Task<Book> GetBookAsync(Guid id)
        {
            return await _bookRepository
              .FindByIdAsync(id);
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _bookRepository
              .FindAllAsync();
        }

        public async Task<bool> UpdateBookAsync(Guid id, Book book)
        {
            return await _bookRepository
              .UpdateAsync(id, book);
        }
    }
}