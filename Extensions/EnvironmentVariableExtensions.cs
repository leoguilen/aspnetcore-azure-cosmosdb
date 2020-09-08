namespace LibraryApi.Extensions
{
    public static class EnvironmentVariableExtensions
    {
        public static string AddMissingCaracters(this string envValue) =>
            envValue.EndsWith("==") ? envValue : 
                envValue.Insert(envValue.Length, "==");
    }
}