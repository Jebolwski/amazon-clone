
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.ViewModels.CartM
{
    public class CartWithProductModel
    {
        public List<ProductResponseModel> products { get; set; }
        public Cart cart { get; set; }
    }
}