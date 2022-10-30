using Argo.Shop.Domain.Common;

namespace Argo.Shop.Domain.Catalog
{
    public class ProductImage : Entity<int>
    {
        public int ProductId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
    }
}
