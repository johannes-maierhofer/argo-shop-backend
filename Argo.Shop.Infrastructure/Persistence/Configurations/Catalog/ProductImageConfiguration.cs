﻿using Argo.Shop.Domain.Catalog.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argo.Shop.Infrastructure.Persistence.Configurations.Catalog
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages", "Catalog");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FileName)
                .IsRequired();
        }
    }
}
