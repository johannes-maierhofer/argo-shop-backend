﻿using Argo.Shop.Domain.Catalog;

namespace Argo.Shop.Infrastructure.Persistence
{
    public static class AppDbContextSeed
    {
        public static async Task SeedSampleDataAsync(AppDbContext context)
        {
            if (!context.Catalog.Products.Any())
            {
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

                await context.SaveChangesAsync();
            }
        }
    }
}