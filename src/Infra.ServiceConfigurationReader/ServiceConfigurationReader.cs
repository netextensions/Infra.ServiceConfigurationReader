using System;
using Microsoft.Extensions.Configuration;

namespace NetExtensions
{
    public class ServiceConfigurationReader
    {
        private const string AppSettings = "appsettings.json";
        public static IConfiguration CreateConfiguration(params string[] jsonFileNames)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(AppSettings, false, true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
                .AddEnvironmentVariables();

            foreach (var fileName in jsonFileNames)
                builder.AddJsonFile(fileName);

            return builder.Build();
        }
    }
}