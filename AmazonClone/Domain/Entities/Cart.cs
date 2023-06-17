namespace AmazonClone.Domain.Entities
{
    public class Cart : Entity
    {
        public ICollection<Product> products { get; set; }
        public Guid userId { get; set; }
    }
}
