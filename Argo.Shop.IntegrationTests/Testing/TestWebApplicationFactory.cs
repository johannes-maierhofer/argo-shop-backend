using Microsoft.AspNetCore.Hosting;
using Argo.Shop.Application.Common.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Argo.Shop.IntegrationTests.Testing.Services;

namespace Argo.Shop.IntegrationTests.Testing
{
    internal class TestWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                var integrationConfig = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .AddEnvironmentVariables()
                    .Build();

                configurationBuilder.AddConfiguration(integrationConfig);
            });

            builder.ConfigureServices((_, services) =>
            {
                services.AddSingleton<TestScopeAccessor>();

                // test specific fakes, mocks, stubs
                services.AddScoped<ICurrentUserService, FakeCurrentUserService>();

                //services.AddScoped<IEmailSender, FakeEmailSender>();
                //services.Decorate<IDomainEventPublisher, TestScopeDomainEventPublisherDecorator>();

                // add logging
                //services.AddSingleton<ILoggerFactory, TestScopeLoggerFactory>();
                //services.Add(ServiceDescriptor.Singleton(typeof(ILogger<>), typeof(Logger<>)));
                //services.AddSingleton(new LoggingConfig // config for Divergic.Logging.Xunit
                //{
                //    IgnoreTestBoundaryException = true,
                //    LogLevel = LogLevel.Information // you can change that to Debug if needed
                //});
            });

            builder.UseEnvironment("Test");
        }
    }
}
