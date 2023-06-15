using Argo.Shop.Application;
using Argo.Shop.Application.Common.Identity;
using Argo.Shop.Infrastructure;
using Argo.Shop.Infrastructure.Persistence;
using Argo.Shop.IntegrationTests.Services;
using Argo.Shop.IntegrationTests.TestData;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Respawn.Graph;

namespace Argo.Shop.IntegrationTests
{
    public static class Testing
    {
        private static IConfigurationRoot _configuration = null!;

        private static IServiceScopeFactory _scopeFactory = null!;
        private static Respawner _respawner = null!;
        public static string? CurrentUserId;

        public static void ConfigureApplication()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            var services = new ServiceCollection();
            services
                .AddApplication()
                .AddInfrastructure(_configuration)
                .AddLogging();

            services.AddScoped<ICurrentUserService, FakeCurrentUserService>();
            services.AddAuthorization();
            
            _scopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();
        }

        public static void RunAsDefaultUser()
        {
            CurrentUserId = UserData.DefaultUserId;
        }

        public static void RunAsAdministrator()
        {
            CurrentUserId = UserData.AdminUserId;
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            return await mediator.Send(request);
        }

        public static async Task EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            // respawner creates a data delete script
            _respawner = await Respawner.CreateAsync(
                _configuration.GetConnectionString("DefaultConnection"),
                new RespawnerOptions
                {
                    TablesToIgnore = new Table[] { "__EFMigrationsHistory" }
                });

            // in the future we might also check out Reseed from https://github.com/uladz-zubrycki/Reseed
        }

        public static async Task ReseedSampleData()
        {
            // deletes existing data according to RespawnerOptions
            await _respawner.ResetAsync(_configuration.GetConnectionString("DefaultConnection"));
            await SeedSampleData();
        }
        
        public static async Task SeedSampleData()
        {
            await DbContextSeed.SeedSampleData(_scopeFactory);
        }
    }
}