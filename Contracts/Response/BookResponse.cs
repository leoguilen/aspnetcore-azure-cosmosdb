using LibraryApi.Models;
using System;

namespace LibraryApi.Contracts.Response
{
    public class BookResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Year { get; set; }
        public string PublishedIn { get; set; }
        public Author Author { get; set; }
    }
}