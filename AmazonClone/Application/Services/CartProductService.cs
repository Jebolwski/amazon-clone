using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.CartProduct;
using AmazonClone.Application.ViewModels.CartProductM;
using AmazonClone.Domain.Interfaces;
using AmazonClone.Domain.Entities;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Application.ViewModels.CartM;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace AmazonClone.Application.Services
{
    public class CartProductService : ICartProductService
    {
        private readonly ICartProductRepository cartProductRepository;
        private readonly IProductService productService;
        private readonly IUserService userService;

        public CartProductService(ICartProductRepository cartProductRepository, IProductService productService, IUserService userService)
        {
            this.cartProductRepository = cartProductRepository;
            this.productService = productService;
            this.userService = userService;
        }

        public CartResponseModel add(CartProductCreateModel model, string authToken)
        {
            if (model != null)
            {
                authToken = authToken.Replace("Bearer ", string.Empty);
                var stream = authToken;
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jsonToken = handler.ReadJwtToken(stream);
                User user = userService.getUserByUsername(jsonToken.Claims.First().Value);
                if (user == null)
                {
                    return null;
                }
                CartProduct cartProduct = cartProductRepository.add(new CartProduct()
                {
                    cartId = user.cartId,
                    productId = model.productId
                });

                ICollection<ProductResponseModel> productResponses = getProductsByCartId(cartProduct.cartId);

                return new CartResponseModel()
                {
                    id = cartProduct.cartId,
                    userId = user.id,
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
