using AmazonClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmazonClone.Data.Mapping
{
    public class CreditCartMap : IEntityTypeConfiguration<CreditCart>
    {
        public void Configure(EntityTypeBuilder<CreditCart> builder)
        {
            builder.ToTable("CreditCart");
            builder.Property(p => p.id).HasColumnName("id");
            builder.Property(p => p.cartNumber).HasColumnName("cartNumber");
            builder.Property(p => p.cvvNumber).HasColumnName("cvvNumber");
            builder.Property(p => p.expDate).HasColumnName("expDate");
            builder.Property(p => p.nameSurname).HasColumnName("nameSurname");
            builder.Property(p => p.userId).HasColumnName("user_id");
        }
    }
}
