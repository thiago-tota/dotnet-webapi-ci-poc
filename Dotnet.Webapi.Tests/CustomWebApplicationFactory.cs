using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace dotnet.webapi.tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Example of how to replace services with NSubstitute if needed
            // For example, to replace a logger with a substitute:
            /*
            var loggerDescriptors = services.Where(d => d.ServiceType.Name.Contains("ILogger")).ToList();
            foreach (var descriptor in loggerDescriptors)
            {
                services.Remove(descriptor);
            }
            services.AddSingleton(typeof(ILogger<>), typeof(SubstituteLogger<>));
            */
        });
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        // Ensure any setup is done before the tests are run
        return base.CreateHost(builder);
    }
}