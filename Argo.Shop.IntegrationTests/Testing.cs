using Argo.Shop.Application;
using Argo.Shop.Domain.Catalog;
using Argo.Shop.Infrastructure;
using Argo.Shop.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Argo.Shop.IntegrationTests
{
    public class Testing
    {
        private static IConfigurationRoot _configuration = null!;

        private static IServiceScopeFactory _scopeFactory = null!;
        //private static Checkpoint _checkpoint = null!;
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

        public static void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            return await mediator.Send(request);
        }

        public static void SeedSampleData()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            context.Catalog.Products.AddRange(new Product
                {
                    Name = "Adidas Stan Smith",
                    Category = "Shoes",
                    Price = 90,
                    Description = "Description for Adidas Stan Smith"
                },
                new Product
                {
                    Name = "Nike Air Max",
                    Category = "Shoes",
                    Price = 110,
                    Description = "Description for Nike Air Max"
                },
                new Product
                {
                    Name = "Reebok Sweat Shirt",
                    Category = "Clothes",
                    Price = 45,
                    Description = "Description for Reebok Sweat Shirt"
                },
                new Product
                {
                    Name = "Puma T-Shirt",
                    Category = "Clothes",
                    Price = 30,
                    Description = "Description for Puma T-Shirt"
                },
                new Product
                {
                    Name = "Under Armour",
                    Category = "Shoes",
                    Price = 130,
                    Description = "Description for Under Armour"
                },
                new Product
                {
                    Name = "Nike Sweat shirt",
                    Category = "Clothes",
                    Price = 65,
                    Description = "Description for Nike Sweat shirt"
                },
                new Product
                {
                    Name = "Spalding basketball",
                    Category = "Gear",
                    Price = 45,
                    Description = "Description for Spalding basketball"
                },
                new Product
                {
                    Name = "Dumbbell 5kg",
                    Category = "Gear",
                    Price = 3.5m,
                    Description = "Description for Dumbbell 5kg"
                },
                new Product
                {
                    Name = "New Balance",
                    Category = "Shoes",
                    Price = 120,
                    Description = "Description for New Balance"
                });

            context.SaveChanges();
        }
    }
}