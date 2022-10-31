namespace Argo.Shop.Application.Features.Catalog.Product.Models
{
    public class ProductListView
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}
