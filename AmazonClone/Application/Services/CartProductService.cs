using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.CartProduct;
using AmazonClone.Application.ViewModels.CartProductM;
using AmazonClone.Domain.Interfaces;
using AmazonClone.Domain.Entities;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Application.ViewModels.CartM;
using System.Collections.Generic;

namespace AmazonClone.Application.Services
{
    public class CartProductService : ICartProductService
    {
        private readonly ICartProductRepository cartProductRepository;
        private readonly IProductService productService;

        public CartProductService(ICartProductRepository cartProductRepository, IProductService productService)
        {
            this.cartProductRepository = cartProductRepository;
            this.productService = productService;
        }

        public CartResponseModel add(CartProductCreateModel model)
        {
            if (model != null)
            {
                CartProduct cartProduct = cartProductRepository.add(new CartProduct()
                {
                    cartId = model.cartId,
                    productId = model.productId
                });

                ICollection<ProductResponseModel> productResponses = getProductsByCartId(cartProduct.cartId);

                return new CartResponseModel()
                {
                    id = model.cartId,
                    products = productResponses
                };
            }
            return null;
        }

        public ICollection<ProductResponseModel> getProductsByCartId(Guid id)
        {
            ICollection<CartProduct> cartProducts = 
                cartProductRepository.getByCartId(id);
            ICollection<ProductResponseModel> productResponseModels = new HashSet<ProductResponseModel>();

            foreach (CartProduct item in cartProducts)
            {
                productResponseModels.Add(productService.get(item.productId));
            }

            return productResponseModels;
        }
    }
}
