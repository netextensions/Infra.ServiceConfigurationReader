using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace NetExtensions.Infra.ServiceConfigurationReader
{
    public class ServiceConfigurationReader
    {
        private const string AppSettings = "appsettings.json";
        public static IConfiguration CreateConfiguration(params string[] jsonFileNames)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile(AppSettings, false, true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
                .AddEnvironmentVariables();

            foreach (var fileName in jsonFileNames)
                builder.AddJsonFile(fileName);

            return builder.Build();
        }
    }
}