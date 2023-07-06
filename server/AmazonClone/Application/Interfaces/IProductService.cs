using AmazonClone.Application.ViewModels.CartM;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Application.ViewModels.ResponseM;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.Application.Interfaces
{
    public interface IProductService
    {
        public ResponseViewModel get(Guid id);
        public ResponseViewModel delete(Guid id);
        public ResponseViewModel update(ProductUpdateModel model);
        public ResponseViewModel add(ProductCreateModel model);
        public ResponseViewModel filterProductsByName(string productName);
        //add product to cart
        public CartResponseModel addProductToCart(ProductCreateModel model);

    }
}
