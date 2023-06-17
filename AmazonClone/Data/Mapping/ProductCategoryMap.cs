using AmazonClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmazonClone.Data.Mapping
{
    public class ProductCategoryMap : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("product_category");
            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.description).HasColumnName("description");
            builder.Property(p => p.name).HasColumnName("name");

        }
    }
}
