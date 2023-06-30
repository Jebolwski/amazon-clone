namespace AmazonClone.Domain.Entities
{
    public class CartProduct : Entity
    {
        public Guid cartId { get; set; }
        public Guid productId { get; set; }
    }
}
