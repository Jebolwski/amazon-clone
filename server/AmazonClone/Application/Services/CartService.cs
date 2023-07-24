using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.CartM;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Application.ViewModels.CartProductM;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;
using AmazonClone.Application.ViewModels.ResponseM;
using System.IdentityModel.Tokens.Jwt;

namespace AmazonClone.Application.Services
{
    public class CartService : ICartService
    {
        private readonly IUserService userService;
        private readonly ICartRepository cartRepository;
        private readonly ICartProductService cartProductService;

        public CartService(ICartRepository cartRepository, ICartProductService cartProductService, IUserService userService)
        {
            this.cartRepository = cartRepository;
            this.cartProductService = cartProductService;
            this.userService = userService;
        }

        public ResponseViewModel addCartToUser(Guid id)
        {
            if (id != null)
            {
                Cart cart = cartRepository.add(new Cart()
                {
                    userId = id,
                });
                return new ResponseViewModel()
                {
                    message = "Kart eklendi. 😍",
                    responseModel = new CartResponseModel()
                    {
                        id = cart.id,
                        userId = id,
                        products = new HashSet<ProductResponseModel>()
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

        public ResponseViewModel addToCart(CartProductCreateModel model, string authToken)
        {
            return cartProductService.add(model, authToken);
        }

        public ResponseViewModel getCart(string authToken)
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
            Cart cart = cartRepository.getCartByUserId(user.id);
            ResponseViewModel responseViewModel = cartProductService.getProductsByCartId(cart.id);
            if (responseViewModel == null)
            {
                return new ResponseViewModel()
                {
                    message = "Ürünler yok. 😞",
                    responseModel = new Object(),
                    statusCode = 400
                };
            }
            return new ResponseViewModel()
            {
                message = "Kart başarıyla getirildi. 🌝",
                responseModel = new
                {
                    products = responseViewModel.responseModel,
                    cart = cart
                },
                statusCode = 200
            };
        }

        public ResponseViewModel deleteProductFromCart(string authToken, Guid productId, Guid cartId)
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
            System.Console.WriteLine(user.id);
            Cart cart = cartRepository.getCartByUserId(user.id);
            System.Console.WriteLine(cart.id + " " + cart.userId);
            if (cart.id != cartId)
            {
                return new ResponseViewModel()
                {
                    message = "Kullanıcı doğrulanamadı. 😞",
                    responseModel = new Object(),
                    statusCode = 400
                };
            }
            if (cartProductService.removeProductFromCart(cartId, productId).statusCode == 200)
            {
                return new ResponseViewModel()
                {
                    message = "Ürün başarıyla karttan kaldırıldı. 🌝",
                    responseModel = new Object(),
                    statusCode = 200
                };
            }
            else
            {
                return new ResponseViewModel()
                {
                    message = "Ürün kaldırılırken hata oluştu. 😥",
                    responseModel = new Object(),
                    statusCode = 400
                };
            }
        }


    }
}
