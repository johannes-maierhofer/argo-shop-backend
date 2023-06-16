using Argo.Shop.Domain.Catalog.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argo.Shop.Infrastructure.Persistence.Configurations.Catalog
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", "Catalog");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired();

            builder.Property(p => p.Category)
                .IsRequired();

            builder.Property(p => p.Price)
                .HasPrecision(14, 2);

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            builder.HasMany(p => p.Images)
                .WithOne()
                .HasForeignKey(pi => pi.ProductId);

            builder.HasIndex(p => p.Name).IsUnique();
            builder.HasIndex(p => p.Category);
        }
    }
}
