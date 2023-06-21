using AmazonClone.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.Data.Mapping
{
    public class ProductProductCategoryMap : IEntityTypeConfiguration<ProductProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductProductCategory> builder)
        {
            builder.ToTable("ProductProductCategory");
            builder.Property(p => p.id).HasColumnName("id");
            builder.Property(p => p.productCategoryId).HasColumnName("productCategoryId");
            builder.Property(p => p.productId).HasColumnName("productId");
        }
    }
}
