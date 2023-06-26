using AmazonClone.Application.ViewModels.CartM;
using AmazonClone.Application.ViewModels.CartProduct;
using AmazonClone.Application.ViewModels.CartProductM;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Application.ViewModels.ProductProductCategory;
using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.Interfaces
{
    public interface ICartProductService
    {
        public CartResponseModel add(CartProductCreateModel model);
        public ICollection<ProductResponseModel> getProductsByCartId(Guid id);
    }
}
