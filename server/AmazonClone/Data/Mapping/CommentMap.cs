﻿using AmazonClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmazonClone.Data.Mapping
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comment");
            builder.Property(p => p.id).HasColumnName("id");
            builder.Property(p => p.userId).HasColumnName("user_id");
            builder.Property(p => p.comment).HasColumnName("comment");
            builder.HasMany(p => p.commentPhotos).WithOne().HasForeignKey(p => p.commentId);
        }
    }
}
