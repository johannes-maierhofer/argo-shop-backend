using Argo.Shop.Application;
using Argo.Shop.Domain.Catalog;
using Argo.Shop.Infrastructure;
using Argo.Shop.Infrastructure.Persistence;
using Argo.Shop.IntegrationTests.TestHelpers;
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
        //private static string? _currentUserId;

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

            _scopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();
        }

        public static async Task EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            _respawner = await Respawner.CreateAsync(
                _configuration.GetConnectionString("DefaultConnection"),
                new RespawnerOptions
                {
                    TablesToIgnore = new Table[] { "__EFMigrationsHistory" }
                });
        }

        public static async Task ReseedSampleData()
        {
            // deletes existing data according to RespawnerOptions
            await _respawner.ResetAsync(_configuration.GetConnectionString("DefaultConnection"));
            await SeedSampleData();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            return await mediator.Send(request);
        }

        public static async Task SeedSampleData()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            context.Catalog.Products.AddRange(new Product
                {
                    Id = 1,
                    Name = "Adidas Stan Smith",
                    Category = "Shoes",
                    Price = 90,
                    Description = "Description for Adidas Stan Smith"
                },
                new Product
                {
                    Id = 2,
                    Name = "Nike Air Max",
                    Category = "Shoes",
                    Price = 110,
                    Description = "Description for Nike Air Max"
                },
                new Product
                {
                    Id = 3,
                    Name = "Reebok Sweat Shirt",
                    Category = "Clothes",
                    Price = 45,
                    Description = "Description for Reebok Sweat Shirt"
                },
                new Product
                {
                    Id = 4,
                    Name = "Puma T-Shirt",
                    Category = "Clothes",
                    Price = 30,
                    Description = "Description for Puma T-Shirt"
                },
                new Product
                {
                    Id = 5,
                    Name = "Under Armour",
                    Category = "Shoes",
                    Price = 130,
                    Description = "Description for Under Armour"
                },
                new Product
                {
                    Id = 6,
                    Name = "Nike Sweat shirt",
                    Category = "Clothes",
                    Price = 65,
                    Description = "Description for Nike Sweat shirt"
                },
                new Product
                {
                    Id = 7,
                    Name = "Spalding basketball",
                    Category = "Gear",
                    Price = 45,
                    Description = "Description for Spalding basketball"
                },
                new Product
                {
                    Id = 8,
                    Name = "Dumbbell 5kg",
                    Category = "Gear",
                    Price = 3.5m,
                    Description = "Description for Dumbbell 5kg"
                },
                new Product
                {
                    Id = 9,
                    Name = "New Balance",
                    Category = "Shoes",
                    Price = 120,
                    Description = "Description for New Balance"
                });

            await context.SaveChangesWithIdentityInsertAsync<Product>();
        }
    }
}