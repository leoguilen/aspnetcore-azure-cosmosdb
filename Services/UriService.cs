using LibraryApi.Contracts;
using System;

namespace LibraryApi.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            this._baseUri = baseUri;
        }

        public Uri GetBookUri(string bookId)
        {
            return new Uri(_baseUri + Routes.Book.Get.Replace("{bookId}", bookId));
        }
    }
}