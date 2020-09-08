using Newtonsoft.Json;
using System;

namespace LibraryApi.Models
{
    public class Book
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "bookId")]
        public Guid BookId { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "category")]
        public string Category { get; set; }

        [JsonProperty(PropertyName = "year")]
        public int Year { get; set; }

        [JsonProperty(PropertyName = "publishedIn")]
        public string PublishedIn { get; set; }

        [JsonProperty(PropertyName = "author")]
        public Author Author { get; set; }
    }
}