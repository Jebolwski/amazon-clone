namespace AmazonClone.Application.ViewModels.CartProductM
{
    public class CartProductCreateModel
    {
        public Guid productId { get; set; }
        public int count { get; set; }
        public bool status { get; set; }
    }
}