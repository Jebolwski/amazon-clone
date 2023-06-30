using AmazonClone.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.Data.Mapping
{
    public class CartProductMap : IEntityTypeConfiguration<CartProduct>
    {
        public void Configure(EntityTypeBuilder<CartProduct> builder)
        {
            builder.ToTable("CartProduct");
            builder.Property(p => p.id).HasColumnName("id");
            builder.Property(p => p.productId).HasColumnName("productId");
            builder.Property(p => p.cartId).HasColumnName("cartId");
        }
    }
}
