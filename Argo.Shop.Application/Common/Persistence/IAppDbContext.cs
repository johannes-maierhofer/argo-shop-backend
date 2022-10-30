using Argo.Shop.Domain.Catalog;
using Microsoft.EntityFrameworkCore;

namespace Argo.Shop.Application.Common.Persistence
{
    public interface IAppDbContext
    {
        public ICatalogSchema Catalog { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

    public interface ICatalogSchema
    {
        public DbSet<Product> Products { get; }
        public DbSet<ProductImage> ProductImages { get; }
    }
}
