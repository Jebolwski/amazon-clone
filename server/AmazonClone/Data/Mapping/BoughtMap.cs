using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using AmazonClone.Domain.Entities;

namespace AmazonClone.Data.Mapping
{
    public class BoughtMap : IEntityTypeConfiguration<Bought>
    {
        public void Configure(EntityTypeBuilder<Bought> builder)
        {
            builder.ToTable("Bougth");
            builder.Property(p => p.id).HasColumnName("id");
            builder.Property(p => p.userId).HasColumnName("userId");
            builder.Property(p => p.timeBought).HasColumnName("timeBought");
            builder.Property(p => p.archived).HasColumnName("archived");
            builder.HasMany(p => p.products).WithOne().HasForeignKey(p => p.boughtId);
        }
    }
}
