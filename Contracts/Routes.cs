namespace LibraryApi.Contracts
{
    public static class Routes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Book
        {
            public const string GetAll = Base + "/books";
            public const string Create = Base + "/books";
            public const string Get = Base + "/books/{bookId}";
            public const string Update = Base + "/books/{bookId}";
            public const string Delete = Base + "/books/{bookId}";
        }
    }
}