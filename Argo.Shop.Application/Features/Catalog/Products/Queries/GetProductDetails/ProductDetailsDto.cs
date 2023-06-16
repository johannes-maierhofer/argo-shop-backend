namespace Argo.Shop.Application.Features.Catalog.Products.Queries.GetProductDetails
{
    public class ProductDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<ProductImageListDto> Images { get; set; } = new();

        public class ProductImageListDto
        {
            public required string FileName { get; set; }
            public bool IsPrimary { get; set; }
        }
    }
}
