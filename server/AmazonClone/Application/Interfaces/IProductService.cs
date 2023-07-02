using AmazonClone.Application.ViewModels.CartM;
using AmazonClone.Application.ViewModels.ProductM;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.Application.Interfaces
{
    public interface IProductService
    {
        public ProductResponseModel get(Guid id);
        public string delete(Guid id);
        public string update(ProductUpdateModel model);
        public string add(ProductCreateModel model);
        public ICollection<ProductResponseModel> filterProductsByName(string productName);
        //add product to cart
        public CartResponseModel addProductToCart(ProductCreateModel model);

    }
}
