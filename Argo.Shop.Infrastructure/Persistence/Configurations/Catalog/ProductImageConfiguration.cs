using Argo.Shop.Domain.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argo.Shop.Infrastructure.Persistence.Configurations.Catalog
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImage", "Catalog");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FileName)
                .IsRequired()
                .HasMaxLength(256);
        }
    }
}
