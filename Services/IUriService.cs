using System;

namespace LibraryApi.Services
{
    public interface IUriService
    {
        Uri GetBookUri(string bookId);
    }
}