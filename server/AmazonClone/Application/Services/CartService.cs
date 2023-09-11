using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.CartM;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Application.ViewModels.CartProductM;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;
using AmazonClone.Application.ViewModels.ResponseM;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using System.Net.Mail;
using System.Net;
namespace AmazonClone.Application.Services
{
    public class CartService : ICartService
    {
        private readonly IUserService userService;
        private readonly ICartRepository cartRepository;
        private readonly ICartProductService cartProductService;
        private readonly IProductService productService;

        public CartService(ICartRepository cartRepository, ICartProductService cartProductService, IUserService userService, IProductService productService = null)
        {
            this.cartRepository = cartRepository;
            this.cartProductService = cartProductService;
            this.userService = userService;
            this.productService = productService;
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
            Cart cart = cartRepository.getCartByUserId(user.id);
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

        public ResponseViewModel buyTheCart(string authToken, Guid cartId)
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
            if (cart.id != cartId)
            {
                return new ResponseViewModel()
                {
                    message = "Kullanıcı doğrulanamadı. 😞",
                    responseModel = new Object(),
                    statusCode = 400
                };
            }
            if (this.getCart(authToken).statusCode == 200)
            {
                string json = JsonSerializer
                    .Serialize(this.getCart(authToken).responseModel);
                CartWithProductModel cartWith = null;
                ResponseViewModel responseViewModel = cartProductService.getProductsByCartId(cartId);
                string extra = "";
                ICollection<ProductResponseModel> products = (ICollection<ProductResponseModel>)responseViewModel.responseModel;
                foreach (ProductResponseModel product in products)
                {
                    extra += "<p style='font-size:16px;text-align:left;margin-top:3px;'>" + product.name + "</p>";
                }
                string body = """
                <!DOCTYPE html>
                    <html lang="en">
                      <head>
                        <meta charset="UTF-8" />
                        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
                        <title>Document</title>
                        <style>
                          * {
                            padding: 0;
                            margin: 0;
                            box-sizing: border-box;
                          }

                          body {
                            padding: 20px;
                            font-family: sans-serif;
                            background-color: rgb(218, 218, 218);
                          }

                          .big {
                            padding: 20px;
                            border-radius: 15px 15px 0px 0px;
                            -webkit-box-shadow: 10px 10px 5px 0px rgba(199, 199, 199, 1);
                            -moz-box-shadow: 10px 10px 5px 0px rgba(199, 199, 199, 1);
                            box-shadow: 10px 10px 5px 0px rgba(199, 199, 199, 1);
                            background: #e8e8e8;
                          }

                          .small {
                            background-color: rgb(218, 218, 218);
                            border-radius: 15px;
                            padding: 15px;
                            margin-top: 10px;
                            -webkit-box-shadow: 5px 0px 5px 0px rgba(199, 199, 199, 1);
                            -moz-box-shadow: 5px 0px 5px 0px rgba(199, 199, 199, 1);
                            box-shadow: 5px 0px 5px 0px rgba(199, 199, 199, 1);
                          }
                          .bottom {
                            background-color: #232f3e;
                            padding: 10px;
                            color: white;
                            border-radius: 0px 0px 15px 15px;
                          }
                        </style>
                      </head>
                      <body>
                        <div class="big" style="font-size: 20px; text-align: center">
                          <p>Ürünleriniz başarıyla satın alınmıştır. 🌝🚀</p>
                          <div class="small">
                            <p style="text-align: left; font-weight: 500">Ürünler</p>
                            <hr />
                            <div>
                """
                +
                    extra
                +
                """
                            </div>
                          </div>
                        </div>
                        <div class="bottom" style="text-align: center; font-size: 18px">
                          Amazon Ekibi
                        </div>
                      </body>
                    </html>
                """;



                if (json.Equals("{}") == false)
                {
                    cartWith = JsonSerializer.Deserialize<CartWithProductModel>(json);
                }
                foreach (ProductResponseModel product in cartWith.products)
                {
                    cartProductService.removeProductFromCart(cartId, product.id);
                }

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("besevler.mah.muh@gmail.com", "nzbhgrczqjtqnnfp"),
                    EnableSsl = true,
                };


                var mail = new MailMessage();
                mail.Subject = "Ürünler başarıyla satın alındı. 🥰";
                mail.From = new MailAddress("besevler.mah.muh@gmail.com");
                mail.To.Add("mertogoko4@gmail.com");
                mail.Body = body;
                mail.IsBodyHtml = true;
                smtpClient.Send(mail);


                return new ResponseViewModel()
                {
                    message = "Karttaki ürünler başarıyla satın alındı. 🌝",
                    responseModel = new Object(),
                    statusCode = 200
                };
            }
            else
            {
                return new ResponseViewModel()
                {
                    message = "Satın alınırken hata oluştu. 😥",
                    responseModel = new Object(),
                    statusCode = 400
                };
            }
        }

        public ResponseViewModel buyTheCartNow(string authToken, Guid cartId, Guid productId)
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
            if (cart.userId != cartId)
            {
                return new ResponseViewModel()
                {
                    message = "Kullanıcı doğrulanamadı. 😞",
                    responseModel = new Object(),
                    statusCode = 400
                };
            }
            if (this.getCart(authToken).statusCode == 200)
            {
                string json = JsonSerializer
                    .Serialize(this.getCart(authToken).responseModel);
                CartWithProductModel cartWith = null;

                ResponseViewModel responseViewModel = cartProductService.getProductsByCartId(cart.id);

                ICollection<ProductResponseModel> products = (ICollection<ProductResponseModel>)responseViewModel.responseModel;
                foreach (ProductResponseModel product in products)
                {
                    cartProductService.removeProductFromCart(cart.id, product.id);
                }

                ResponseViewModel responseViewModel2 = this.addToCart(new CartProductCreateModel()
                {
                    productId = productId
                }, authToken);

                return new ResponseViewModel()
                {
                    message = "Karttaki ürün başarıyla eklendi. 🌝",
                    responseModel = new Object(),
                    statusCode = 200
                };
            }
            else
            {
                return new ResponseViewModel()
                {
                    message = "Eklenirken hata oluştu. 😥",
                    responseModel = new Object(),
                    statusCode = 400
                };
            }
        }

        public ResponseViewModel getCartByUserId(Guid userId)
        {
            Cart cart = cartRepository.getCartByUserId(userId);
            return new ResponseViewModel()
            {
                message = "Kart başarıyla getirildi. 🌝",
                responseModel = cart,
                statusCode = 200
            };
        }
    }
}
