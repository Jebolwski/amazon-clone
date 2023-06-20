namespace AmazonClone.Domain.Entities
{
    public class CommentPhoto : Entity
    {
        public string photoUrl { get; set; }
        public Comment comment { get; set; }


    }
}
