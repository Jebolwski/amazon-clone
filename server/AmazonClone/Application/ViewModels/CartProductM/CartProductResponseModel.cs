using AmazonClone.Application.ViewModels.ProductM;

namespace AmazonClone.Application.ViewModels.CartProduct
{
    public class CartProductResponseModel
    {
        public Guid id { get; set; }
        public ICollection<ProductResponseModel> products { get; set; }
    }
}