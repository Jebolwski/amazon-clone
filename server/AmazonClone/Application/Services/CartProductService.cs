using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.CartProduct;
using AmazonClone.Application.ViewModels.CartProductM;
using AmazonClone.Domain.Interfaces;
using AmazonClone.Domain.Entities;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Application.ViewModels.CartM;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using AmazonClone.Application.ViewModels.ResponseM;

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

        public ResponseViewModel add(CartProductCreateModel model, string authToken)
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
                    return new ResponseViewModel()
                    {
                        message = "Kullanıcı doğrulanamadı. 😞",
                        responseModel = new Object(),
                        statusCode = 400
                    };
                }
                CartProduct cartProduct = cartProductRepository.add(new CartProduct()
                {
                    cartId = user.cartId,
                    productId = model.productId
                });

                ICollection<ProductResponseModel> productResponses = (ICollection<ProductResponseModel>)getProductsByCartId(cartProduct.cartId).responseModel;
                return new ResponseViewModel()
                {
                    message = "Karta ürün eklendi. 🌝",
                    responseModel = new CartResponseModel()
                    {
                        id = cartProduct.cartId,
                        userId = user.id,
                        products = productResponses
                    },
                    statusCode = 200
                };
            }
            return new ResponseViewModel()
            {
                message = "Veri verilmedi. 😒",
                responseModel = new Object(),
                statusCode = 400
            };
        }

        public ResponseViewModel getProductsByCartId(Guid id)
        {
            ICollection<CartProduct> cartProducts =
                cartProductRepository.getByCartId(id);
            ICollection<ProductResponseModel> productResponseModels = new HashSet<ProductResponseModel>();

            foreach (CartProduct item in cartProducts)
            {
                productResponseModels.Add((ProductResponseModel)productService.get(item.productId).responseModel);
            }

            return new ResponseViewModel()
            {
                message = "Veri getirildi. 🌝",
                responseModel = productResponseModels,
                statusCode = 200
            };
        }

        public ResponseViewModel removeProductFromCart(Guid cartId, Guid productId)
        {
            bool boolean =
                cartProductRepository.deleteByCartIdAndProductId(productId, cartId);
            if (boolean)
            {
                return new ResponseViewModel()
                {
                    message = "Ürün karttan silindi. 🌝",
                    responseModel = new object(),
                    statusCode = 200
                };
            }
            else
            {
                return new ResponseViewModel()
                {
                    message = "Ürün silinirken bir hata oluştu. 😒",
                    responseModel = new object(),
                    statusCode = 400
                };
            }
        }

    }
}
