using AmazonClone.Application.ViewModels.CartProduct;
using AmazonClone.Application.ViewModels.ProductM;

namespace AmazonClone.Application.ViewModels.CartM
{
    public class CartResponseModel
    {
        public Guid id { get; set; }
        public Guid userId { get; set; }
        public ICollection<CartProductProductResponseModel> products { get; set; }
    }
}
