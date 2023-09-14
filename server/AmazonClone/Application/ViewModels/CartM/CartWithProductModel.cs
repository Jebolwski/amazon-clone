
using AmazonClone.Application.ViewModels.CartProduct;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.ViewModels.CartM
{
    public class CartWithProductModel
    {
        public List<CartProductProductResponseModel> products { get; set; }
        public Cart cart { get; set; }
    }
}