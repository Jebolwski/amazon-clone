namespace AmazonClone.Domain.Entities
{
    public class Comment : Entity
    {
        public Guid userId { get; set; }
        public string comment { get; set; }
        public Product product { get; set; }
        public ICollection<CommentPhoto> commentPhotos { get; set; }
    }
}
