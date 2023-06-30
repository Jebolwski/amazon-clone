using AmazonClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmazonClone.Data.Mapping
{
    public class ProductCategoryMap : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategory");
            builder.Property(p => p.id).HasColumnName("id");
            builder.Property(p => p.description).HasColumnName("description");
            builder.Property(p => p.name).HasColumnName("name");
        }
    }
}
