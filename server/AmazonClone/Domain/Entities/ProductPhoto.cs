namespace AmazonClone.Domain.Entities
{
    public class ProductPhoto : Entity
    {
        public string photoUrl { get; set; }
        public Guid productId { get; set; }
    }
}
