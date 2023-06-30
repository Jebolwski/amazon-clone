using AmazonClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmazonClone.Data.Mapping
{
    public class CommentPhotoMap : IEntityTypeConfiguration<CommentPhoto>
    {
        public void Configure(EntityTypeBuilder<CommentPhoto> builder)
        {
            builder.ToTable("CommentPhoto");
            builder.Property(p => p.id).HasColumnName("id");
            builder.Property(p => p.photoUrl).HasColumnName("photo_url");
        }
    }
}
