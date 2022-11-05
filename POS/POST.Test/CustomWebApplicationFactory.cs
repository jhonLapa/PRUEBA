using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace POST.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                var integrationConfiguration = new ConfigurationBuilder()
                .AddJsonFile("appsetings.json")
                .AddEnvironmentVariables()
                .Build();

                configurationBuilder.AddConfiguration(integrationConfiguration);
            });
        }
    }
}
