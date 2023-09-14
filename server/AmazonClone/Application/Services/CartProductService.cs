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
using AmazonClone.Application.ViewModels.ProductCategoryM;
using System.Text.Json;

namespace AmazonClone.Application.Services
{
    public class CartProductService : ICartProductService
    {
        private readonly ICartProductRepository cartProductRepository;
        private readonly ICartRepository cartRepository;
        private readonly IProductService productService;
        private readonly IUserService userService;

        public CartProductService(ICartProductRepository cartProductRepository, IProductService productService, IUserService userService, ICartRepository cartRepository)
        {
            this.cartProductRepository = cartProductRepository;
            this.productService = productService;
            this.userService = userService;
            this.cartRepository = cartRepository;
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
                List<CartProduct> products = new List<CartProduct>();
                for (var i = 0; i < model.count; i++)
                {
                    products.Add(cartProductRepository.add(new CartProduct()
                    {
                        cartId = user.cartId,
                        productId = model.productId,
                        status = true
                    }));
                }

                ICollection<CartProductProductResponseModel> productResponses = (ICollection<CartProductProductResponseModel>)getProductsByCartId(user.cartId).responseModel;
                return new ResponseViewModel()
                {
                    message = "Karta ürün eklendi. 🌝",
                    responseModel = new CartResponseModel()
                    {
                        id = user.cartId,
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
            ICollection<CartProductProductResponseModel> productResponseModels = new HashSet<CartProductProductResponseModel>();

            foreach (CartProduct item in cartProducts)
            {
                ProductResponseModel productResponseModel = (ProductResponseModel)productService.get(item.productId).responseModel;
                productResponseModels.Add(new CartProductProductResponseModel()
                {
                    comments = productResponseModel.comments,
                    description = productResponseModel.description,
                    id = productResponseModel.id,
                    name = productResponseModel.name,
                    photos = productResponseModel.photos,
                    price = productResponseModel.price,
                    productCategories = productResponseModel.productCategories,
                    status = item.status
                });
            }

            return new ResponseViewModel()
            {
                message = "Veri getirildi. 🌝",
                responseModel = productResponseModels,
                statusCode = 200
            };
        }

        public ResponseViewModel getProductsByCartIdStatusOne(Guid id)
        {
            ICollection<CartProduct> cartProducts =
                cartProductRepository.getByCartIdStatusOne(id);
            ICollection<CartProductProductResponseModel> productResponseModels = new HashSet<CartProductProductResponseModel>();

            foreach (CartProduct item in cartProducts)
            {
                ProductResponseModel productResponseModel = (ProductResponseModel)productService.get(item.productId).responseModel;
                productResponseModels.Add(new CartProductProductResponseModel()
                {
                    comments = productResponseModel.comments,
                    description = productResponseModel.description,
                    id = productResponseModel.id,
                    name = productResponseModel.name,
                    photos = productResponseModel.photos,
                    price = productResponseModel.price,
                    productCategories = productResponseModel.productCategories,
                    status = item.status
                });
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

        public ResponseViewModel toggleStatus(Guid cartId, Guid productId)
        {
            Cart cart = cartRepository.getCartByUserId(cartId);
            bool boolean =
                cartProductRepository.toggle(cart.id, productId);
            if (boolean || !boolean)
            {
                return new ResponseViewModel()
                {
                    message = "Ürün durumu başarıyla değiştirildi. 🌝",
                    responseModel = boolean,
                    statusCode = 200
                };
            }
            else
            {
                return new ResponseViewModel()
                {
                    message = "Ürün durumu değiştirilirken bir hata oluştu. 😒",
                    responseModel = new object(),
                    statusCode = 400
                };
            }
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
            ResponseViewModel responseViewModel = getProductsByCartId(cart.id);
            if (responseViewModel == null)
            {
                return new ResponseViewModel()
                {
                    message = "Ürünler yok. 😞",
                    responseModel = new Object(),
                    statusCode = 400
                };
            }
            List<CartProductProductResponseModel> list = new List<CartProductProductResponseModel>();

            string json = JsonSerializer
                .Serialize(responseViewModel.responseModel);
            if (json.Equals("{}") == false)
            {
                list = JsonSerializer.Deserialize<List<CartProductProductResponseModel>>(json);
            }

            return new ResponseViewModel()
            {
                message = "Kart başarıyla getirildi. 🌝",
                responseModel = new CartWithProductModel()
                {
                    products = list,
                    cart = cart
                },
                statusCode = 200
            };
        }

        public ResponseViewModel toggleAllStatusOff(Guid cartId, string authToken)
        {
            ResponseViewModel responseViewModel = getCart(authToken);
            CartWithProductModel response = (CartWithProductModel)(responseViewModel.responseModel);
            foreach (CartProductProductResponseModel product in response.products)
            {
                bool v = cartProductRepository.turnOff(cartId, product.id);
                if (!v)
                {
                    return new ResponseViewModel()
                    {
                        message = "Ürünlerin seçilmesi kaldırılırken hata oluştu. 😞",
                        responseModel = new object(),
                        statusCode = 500
                    };
                }
            }
            return new ResponseViewModel()
            {
                message = "Başarıyla ürünlerin seçilmesi kaldırıldı. 😄",
                responseModel = new object(),
                statusCode = 200
            };

        }

        public ResponseViewModel getByCartIdAndProductId(Guid cartId, Guid productId)
        {
            CartProduct cartProduct = cartProductRepository.getByCartIdAndProductId(cartId, productId);
            if (cartProduct != null)
            {
                return new ResponseViewModel()
                {
                    message = "Başarıyla getirildi. 🌝",
                    responseModel = cartProduct,
                    statusCode = 200
                };
            }
            else
            {
                return new ResponseViewModel()
                {
                    message = "Getirilirken hata oluştu. 😞",
                    responseModel = new object(),
                    statusCode = 400
                };
            }
        }

    }
}
