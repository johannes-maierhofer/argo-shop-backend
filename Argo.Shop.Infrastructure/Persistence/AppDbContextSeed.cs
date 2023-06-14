using Argo.Shop.Domain.Catalog.Products;
using Argo.Shop.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Argo.Shop.Infrastructure.Persistence
{
    public static class AppDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (userManager.Users.All(u => u.UserName != administrator.UserName))
            {
                await userManager.CreateAsync(administrator, "Administrator1!");
                await userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
        }

        public static async Task SeedSampleDataAsync(AppDbContext context)
        {
            if (!context.Catalog.Products.Any())
            {
                context.Catalog.Products.AddRange(new Product
                {
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
                    Name = "White Backpack with Black Logo",
                    Category = "Backpacks",
                    Price = 25,
                    Description = "Medium size backpack with plenty of room plus a big inner pocket, a separate section for a 15'' laptop, a front pocket, and a hidden pocket at the back. Made of a water-resistant material. The soft, padded mesh material on the back and the black handles make it perfect for daily use or sports activities.",
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
                    Name = "Black Baseball Hat with White Logo",
                    Category = "Hats",
                    Price = 9.9m,
                    Description = "Step up your accessory game with a new washed twill dad cap. Pair our embroidery logo design with a sporty feel and create a unique premium baseball hat that's bound to become a favorite.",
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

                await context.SaveChangesAsync();
            }
        }
    }
}