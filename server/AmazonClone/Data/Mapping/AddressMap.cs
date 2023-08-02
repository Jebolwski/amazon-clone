using AmazonClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmazonClone.Data.Mapping
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address");
            builder.Property(p => p.id).HasColumnName("id");
            builder.Property(p => p.apartmentNo).HasColumnName("apartmentNo");
            builder.Property(p => p.apartmentName).HasColumnName("apartmentName");
            builder.Property(p => p.addressComplete).HasColumnName("addressComplete");
            builder.Property(p => p.city).HasColumnName("city");
            builder.Property(p => p.floor).HasColumnName("floor");
            builder.Property(p => p.userId).HasColumnName("user_id");
        }
    }
}
