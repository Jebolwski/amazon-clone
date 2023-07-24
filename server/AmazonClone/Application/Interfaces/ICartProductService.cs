using AmazonClone.Application.ViewModels.CartM;
using AmazonClone.Application.ViewModels.CartProduct;
using AmazonClone.Application.ViewModels.CartProductM;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Application.ViewModels.ProductProductCategory;
using AmazonClone.Application.ViewModels.ResponseM;
using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.Interfaces
{
    public interface ICartProductService
    {
        public ResponseViewModel add(CartProductCreateModel model, string authToken);
        public ResponseViewModel getProductsByCartId(Guid id);
        public ResponseViewModel removeProductFromCart(Guid cartId, Guid productId);
    }
}
