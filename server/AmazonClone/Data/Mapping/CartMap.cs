using AmazonClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmazonClone.Data.Mapping
{
    public class CartMap : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Cart");
            builder.Property(p => p.id).HasColumnName("id");
            builder.Property(p => p.userId).HasColumnName("user_id");
        }
    }
}
