using LibraryApi.Models;

namespace LibraryApi.Contracts.Request
{
    public class UpdateBookRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Year { get; set; }
        public string PublishedIn { get; set; }
        public Author Author { get; set; }
    }
}