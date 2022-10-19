using Microsoft.Extensions.Configuration;

namespace Core.Utilities.Configuration
{
    public class ConfigurationHelper
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }
}
