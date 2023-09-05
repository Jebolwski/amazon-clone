using AmazonClone.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.Data.Mapping
{
    public class RefundMap : IEntityTypeConfiguration<Refund>
    {
        public void Configure(EntityTypeBuilder<Refund> builder)
        {
            builder.ToTable("Refund");
            builder.Property(p => p.id).HasColumnName("id");
            builder.Property(p => p.BoughtId).HasColumnName("BoughtId");
            builder.Property(p => p.RefundCode).HasColumnName("RefundCode");
        }
    }
}
