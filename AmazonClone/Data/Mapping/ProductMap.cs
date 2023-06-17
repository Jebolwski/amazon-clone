using AmazonClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmazonClone.Data.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("product");
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.price).HasColumnName("price");
            builder.Property(p => p.description).HasColumnName("description");
            builder.Property(p => p.name).HasColumnName("name");
            builder.HasMany(p => p.photos).WithOne(p => p.product);
            builder.HasMany(p => p.carts).WithMany(p => p.products);
            builder.HasMany(p => p.productCategories).WithMany(p => p.products);
        }
    }
}
