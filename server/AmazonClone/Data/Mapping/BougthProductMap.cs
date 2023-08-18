using AmazonClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmazonClone.Data.Mapping
{
    public class BougthProductMap : IEntityTypeConfiguration<BoughtProduct>
    {
        public void Configure(EntityTypeBuilder<BoughtProduct> builder)
        {
            builder.ToTable("BougthProduct");
            builder.Property(p => p.id).HasColumnName("id");
            builder.Property(p => p.productId).HasColumnName("productId");
            builder.Property(p => p.price).HasColumnName("price");
            builder.Property(p => p.description).HasColumnName("description");
            builder.Property(p => p.name).HasColumnName("name");
        }
    }
}
