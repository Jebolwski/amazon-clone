using AmazonClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmazonClone.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.Property(p => p.id).HasColumnName("id");
            builder.Property(p => p.username).HasColumnName("username");
            builder.Property(p => p.passwordSalt).HasColumnName("passwordSalt");
            builder.Property(p => p.passwordHash).HasColumnName("passwordHash");
            builder.Property(p => p.roleId).HasColumnName("role_id");
        }
    }
}
