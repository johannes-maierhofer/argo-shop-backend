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
                    Name = "Black Five-Panel Cap with White Logo",
                    Category = "Caps",
                    Price = 15,
                    Description =
                        "Soft-structured, five-panel, low-profile cap. 100% cotton, metal eyelets, nylon strap clip closure.",
                    PrimaryImageFileName = "5PANECAP000000FFFFFFXXXX_FLAT.png",
                    Images = new List<ProductImage>
                    {
                        new()
                        {
                            FileName = "5PANECAP000000FFFFFFXXXX_FLAT.png",
                            IsPrimary = true
                        }
                    }
                },
                new Product
                {
                    Id = 2,
                    Name = "Gray Five-Panel Cap with White Logo",
                    Category = "Caps",
                    Price = 19.90m,
                    Description =
                        "Soft-structured, five-panel, low-profile cap. 100% cotton, metal eyelets, nylon strap clip closure.",
                    PrimaryImageFileName = "5PANECAP9D9CA1FFFFFFXXXX_FLAT.png",
                    Images = new List<ProductImage>
                    {
                        new()
                        {
                            FileName = "5PANECAP9D9CA1FFFFFFXXXX_FLAT.png",
                            IsPrimary = true
                        }
                    }
                },
                new Product
                {
                    Id = 3,
                    Name = "Black Apron with White Logo",
                    Category = "Aprons",
                    Price = 45,
                    Description =
                        "This apron has a neck loop and long ties that are easy to adjust for any size. The two front pockets provide additional space for some much-needed cooking utensils, and together with our embroidered logo give the apron a sleek premium look.",
                    PrimaryImageFileName = "APRONXXX000000FFFFFFXXXX_FLAT.png",
                    Images = new List<ProductImage>
                    {
                        new()
                        {
                            FileName = "APRONXXX000000FFFFFFXXXX_FLAT.png",
                            IsPrimary = true
                        }
                    }
                },
                new Product
                {
                    Id = 4,
                    Name = "White Apron with Black Logo",
                    Category = "Aprons",
                    Price = 30,
                    Description =
                        "This apron has a neck loop and long ties that are easy to adjust for any size. The two front pockets provide additional space for some much-needed cooking utensils, and together with our embroidered logo give the apron a sleek premium look.",
                    PrimaryImageFileName = "APRONXXXFFFFFF000000XXXX_FLAT.png",
                    Images = new List<ProductImage>
                    {
                        new()
                        {
                            FileName = "APRONXXXFFFFFF000000XXXX_FLAT.png",
                            IsPrimary = true
                        }
                    }
                },
                new Product
                {
                    Id = 5,
                    Name = "Gray Baby Bib with Black Logo",
                    Category = "Bibs",
                    Price = 9.90m,
                    Description =
                        "Avoid getting food stains on child’s clothes with this baby bib. The reinforced hook & loop closure makes it easy to put on, but hard for baby to take off. 4x4\" embroidered logo.",
                    PrimaryImageFileName = "BABYBIBXA19D9D000000XXXX_FLAT.png",
                    Images = new List<ProductImage>
                    {
                        new()
                        {
                            FileName = "BABYBIBXA19D9D000000XXXX_FLAT.png",
                            IsPrimary = true
                        }
                    }
                },
                new Product
                {
                    Id = 6,
                    Name = "Black Backpack with White Logo",
                    Category = "Backpacks",
                    Price = 65,
                    Description =
                        "Medium size backpack with plenty of room plus a big inner pocket, a separate section for a 15'' laptop, a front pocket, and a hidden pocket at the back. Made of a water-resistant material. The soft, padded mesh material on the back and the black handles make it perfect for daily use or sports activities.",
                    PrimaryImageFileName = "BACKPACK000000FFFFFFXXXX_FLAT.png",
                    Images = new List<ProductImage>
                    {
                        new()
                        {
                            FileName = "BACKPACK000000FFFFFFXXXX_FLAT.png",
                            IsPrimary = true
                        }
                    }
                },
                new Product
                {
                    Id = 7,
                    Name = "Gray Backpack with Black Logo",
                    Category = "Backpacks",
                    Price = 45,
                    Description =
                        "Two-color backpack made from a water-resistant material. It has a soft, padded back and a top carry handle, making it the perfect small size backpack for daily use or sports. Embroidered logo.",
                    PrimaryImageFileName = "BACKPACK818488000000XXXX_FLAT.png",
                    Images = new List<ProductImage>
                    {
                        new()
                        {
                            FileName = "BACKPACK818488000000XXXX_FLAT.png",
                            IsPrimary = true
                        }
                    }
                },
                new Product
                {
                    Id = 8,
                    Name = "White Backpack with Black Logo",
                    Category = "Backpacks",
                    Price = 25,
                    Description =
                        "Medium size backpack with plenty of room plus a big inner pocket, a separate section for a 15'' laptop, a front pocket, and a hidden pocket at the back. Made of a water-resistant material. The soft, padded mesh material on the back and the black handles make it perfect for daily use or sports activities.",
                    PrimaryImageFileName = "BACKPACKFFFFFF000000XXXX_FLAT.png",
                    Images = new List<ProductImage>
                    {
                        new()
                        {
                            FileName = "BACKPACKFFFFFF000000XXXX_FLAT.png",
                            IsPrimary = true
                        }
                    }
                },
                new Product
                {
                    Id = 9,
                    Name = "Black Baseball Hat with White Logo",
                    Category = "Hats",
                    Price = 9.9m,
                    Description =
                        "Step up your accessory game with a new washed twill dad cap. Pair our embroidery logo design with a sporty feel and create a unique premium baseball hat that's bound to become a favorite.",
                    PrimaryImageFileName = "BASEBHAT000000FFFFFFXXXX_FLAT.png",
                    Images = new List<ProductImage>
                    {
                        new()
                        {
                            FileName = "BASEBHAT000000FFFFFFXXXX_FLAT.png",
                            IsPrimary = true
                        }
                    }
                },
                new Product
                {
                    Id = 10,
                    Name = "White Baseball Hat with Black Logo",
                    Category = "Hats",
                    Price = 9.9m,
                    Description = "Step up your accessory game with a new washed twill dad cap. Pair our embroidery logo design with a sporty feel and create a unique premium baseball hat that's bound to become a favorite.",
                    PrimaryImageFileName = "BASEBHATFFFFFF000000XXXX_FLAT.png",
                    Images = new List<ProductImage>
                    {
                        new()
                        {
                            FileName = "BASEBHATFFFFFF000000XXXX_FLAT.png",
                            IsPrimary = true
                        }
                    }
                });

            await context.SaveChangesWithIdentityInsertAsync<Product>();
        }
    }
}