﻿namespace Argo.Shop.Application.Features.Catalog.Products.Queries.GetProductList
{
    public class ProductListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public string? PrimaryImageFileName { get; set; }
    }
}
