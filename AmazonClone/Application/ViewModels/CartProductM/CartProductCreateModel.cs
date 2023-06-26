namespace AmazonClone.Application.ViewModels.CartProductM
{
    public class CartProductCreateModel
    {
        public Guid id { get; set; }
        public Guid cartId { get; set; }
        public Guid productId { get; set; }
    }
}