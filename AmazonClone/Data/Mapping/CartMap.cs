using AmazonClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmazonClone.Data.Mapping
{
    public class CartMap : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("cart");
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.userId).HasColumnName("user_id");
            builder.HasMany(p => p.products).WithMany(p => p.carts);
        }
    }
}
