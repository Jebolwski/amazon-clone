using AmazonClone.Application.ViewModels.CartM;
using AmazonClone.Application.ViewModels.ProductM;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.Application.Interfaces
{
    public interface IProductService
    {
        public ProductResponseModel get(Guid id);
        public bool delete(Guid id);
        public ProductResponseModel update(ProductUpdateModel model);
        public ProductResponseModel add(ProductCreateModel model);

        //add product to cart
        public CartResponseModel addProductToCart(ProductCreateModel model);

    }
}
