using LibraryApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book> GetBookAsync(Guid id);
        Task<bool> AddBookAsync(Book book);
        Task<bool> UpdateBookAsync(Guid id, Book book);
        Task<bool> DeleteBookAsync(Guid id);
    }
}