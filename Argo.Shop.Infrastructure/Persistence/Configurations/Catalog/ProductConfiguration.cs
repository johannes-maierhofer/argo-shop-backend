using Argo.Shop.Domain.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argo.Shop.Infrastructure.Persistence.Configurations.Catalog
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product", "Catalog");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(p => p.Category)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(p => p.Price)
                .HasPrecision(14, 2);

            builder.HasMany(p => p.Images)
                .WithOne()
                .HasForeignKey(pi => pi.ProductId);

            builder.HasIndex(p => p.Category);
            builder.HasIndex(p => p.Name).IsUnique();
        }
    }
}
