using Microsoft.Extensions.Configuration;
using NETCore3WebApp.Domain;

namespace NETCore3WebApp.IntegrationTest
{
    public static class TestHelper
    {
        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public static AppSettings GetApplicationConfiguration(string outputPath)
        {
            var configuration = new AppSettings();

            var iConfig = GetIConfigurationRoot(outputPath);

            iConfig
                .GetSection("Swift")
                .Bind(configuration);

            return configuration;
        }
    }
}
