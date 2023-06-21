namespace AmazonClone.Domain.Entities
{
    public class CartProduct
    {
        public Guid id { get; set; }
        public Guid cartId { get; set; }
        public Guid productId { get; set; }
    }
}
