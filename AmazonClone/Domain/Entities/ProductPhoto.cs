namespace AmazonClone.Domain.Entities
{
    public class ProductPhoto : Entity
    {
        public string photoUrl { get; set; }
        public Product product { get; set; }
    }
}
