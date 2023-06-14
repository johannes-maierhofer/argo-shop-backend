using Argo.Shop.Domain.Common;

namespace Argo.Shop.Domain.Catalog.Products
{
    public class ProductImage : Entity<int>
    {
        public int ProductId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
    }
}
