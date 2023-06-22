using AmazonClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmazonClone.Data.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.Property(p => p.id).HasColumnName("id");
            builder.Property(p => p.price).HasColumnName("price");
            builder.Property(p => p.description).HasColumnName("description");
            builder.Property(p => p.name).HasColumnName("name");
            builder.HasMany(p => p.photos).WithOne().HasForeignKey(p => p.productId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(p => p.comments).WithOne().HasForeignKey(p => p.productId);
        }
    }
}
