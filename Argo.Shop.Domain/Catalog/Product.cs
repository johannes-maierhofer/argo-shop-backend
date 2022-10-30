using Argo.Shop.Domain.Common;

namespace Argo.Shop.Domain.Catalog
{
    public class Product : Entity<int>
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<ProductImage> Images { get; set; } = new();
    }
}
