using System;

namespace Auth
{
    internal class ApplicationEnvironment
    {
        internal static bool IsDevelopment() =>
            GetEnvironment().ToLower() == "development" ? true : false;

        private static string GetEnvironment() =>
            Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
    }
}