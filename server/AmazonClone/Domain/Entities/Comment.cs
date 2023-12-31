﻿namespace AmazonClone.Domain.Entities
{
    public class Comment : Entity
    {
        public Guid userId { get; set; }
        public string title { get; set; }
        public string comment { get; set; }
        public int stars { get; set; }
        public Guid productId { get; set; }
        public ICollection<CommentPhoto> commentPhotos { get; set; }
    }
}
