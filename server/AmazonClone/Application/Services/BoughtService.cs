using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.AuthM;
using AmazonClone.Application.ViewModels.BoughtM;
using AmazonClone.Application.ViewModels.BoughtProductM;
using AmazonClone.Application.ViewModels.ProductCategoryM;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Application.ViewModels.ResponseM;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace AmazonClone.Application.Services
{
    public class BoughtService : IBoughtService
    {
        private readonly ICartService cartService;
        private readonly IBoughtProductService boughtProductService;
        private readonly IBoughtRepository boughtRepository;
        private readonly IUserService userService;

        private readonly ICartProductService cartProductService;

        public BoughtService(ICartService cartService, IBoughtProductService boughtProductService, IBoughtRepository boughtRepository, IUserService userService, ICartProductService cartProductService)
        {
            this.cartService = cartService;
            this.boughtProductService = boughtProductService;
            this.boughtRepository = boughtRepository;
            this.userService = userService;
            this.cartProductService = cartProductService;
        }

        public ResponseViewModel addBought(string authToken)
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
            ResponseViewModel responseViewModel = cartService.getCartStatusOne(authToken);
            Bought boughtCreated = boughtRepository.add(new Bought()
            {
                userId = user.id,
                timeBought = DateTime.UtcNow,
            });
            object responseModel = responseViewModel.responseModel;
            string json = JsonSerializer
                    .Serialize(responseModel);
            ResposneModel response = new ResposneModel();
            if (json.Equals("{}") == false)
            {
                response = JsonSerializer.Deserialize<ResposneModel>(json);
            }


            foreach (ProductResponseModel product in response.products)
            {
                //!CartProduct'tan status getiriliyor
                ResponseViewModel responseViewModel2 = cartProductService.getByCartIdAndProductId(response.cart.id, product.id);
                CartProduct cartProduct = (CartProduct)responseViewModel2.responseModel;
                if (cartProduct.status == true)
                {

                    BoughtProductAddModel boughtProductAddModel = new BoughtProductAddModel()
                    {
                        description = product.description,
                        name = product.name,
                        price = product.price,
                        productId = product.id,
                        boughtId = boughtCreated.id
                    };
                    boughtProductService.addBoughtProduct(boughtProductAddModel);
                }
            }

            return new ResponseViewModel()
            {
                message = "Önceden alınanlara başarıyla eklendi. 🥰",
                responseModel = new object(),
                statusCode = 200
            };
        }

        public ResponseViewModel getBoughts(string authToken)
        {
            authToken = authToken.Replace("Bearer ", string.Empty);
            var stream = authToken;
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jsonToken = handler.ReadJwtToken(stream);
            User user = userService.getUserByUsername(jsonToken.Claims.First().Value);
            List<Bought> boughts = boughtRepository.getAllByUserId(user.id);
            if (boughts != null && boughts.Any())
            {
                List<BoughtResponseModel> boughtResponses = new List<BoughtResponseModel>();
                foreach (Bought bought in boughts)
                {
                    BoughtResponseModel boughtResponseModel = new BoughtResponseModel()
                    {
                        timeBought = bought.timeBought,
                        id = bought.id,
                        user = new UserResponseModel()
                        {
                            cartId = user.cartId,
                            id = user.id,
                            roleId = user.roleId,
                            TokenCreated = user.TokenCreated,
                            TokenExpires = user.TokenExpires,
                            username = user.username
                        },
                        products = (List<BoughtProductResponseModel>)(boughtProductService.ProductsByBoughtId(bought.id).responseModel)
                    };
                    boughtResponses.Add(boughtResponseModel);
                }
                if (user == null)
                {
                    return new ResponseViewModel()
                    {
                        message = "Kullanıcı doğrulanamadı. 😞",
                        responseModel = new Object(),
                        statusCode = 400
                    };
                }

                return new ResponseViewModel()
                {
                    message = "Siparişler listelendi. 🥰",
                    responseModel = boughtResponses,
                    statusCode = 200
                };
            }
            return new ResponseViewModel()
            {
                message = "Siparişler listelendi. 🥰",
                responseModel = new object(),
                statusCode = 400
            };
        }


        public ResponseViewModel getArchivedBoughts(string authToken)
        {
            authToken = authToken.Replace("Bearer ", string.Empty);
            var stream = authToken;
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jsonToken = handler.ReadJwtToken(stream);
            User user = userService.getUserByUsername(jsonToken.Claims.First().Value);
            List<Bought> boughts = boughtRepository.getAllArchivedByUserId(user.id);
            if (boughts != null && boughts.Any())
            {
                List<BoughtResponseModel> boughtResponses = new List<BoughtResponseModel>();
                foreach (Bought bought in boughts)
                {
                    BoughtResponseModel boughtResponseModel = new BoughtResponseModel()
                    {
                        timeBought = bought.timeBought,
                        id = bought.id,
                        user = new UserResponseModel()
                        {
                            cartId = user.cartId,
                            id = user.id,
                            roleId = user.roleId,
                            TokenCreated = user.TokenCreated,
                            TokenExpires = user.TokenExpires,
                            username = user.username
                        },
                        products = (List<BoughtProductResponseModel>)(boughtProductService.ProductsByBoughtId(bought.id).responseModel)
                    };
                    boughtResponses.Add(boughtResponseModel);
                }
                if (user == null)
                {
                    return new ResponseViewModel()
                    {
                        message = "Kullanıcı doğrulanamadı. 😞",
                        responseModel = new Object(),
                        statusCode = 400
                    };
                }

                return new ResponseViewModel()
                {
                    message = "Siparişler listelendi. 🥰",
                    responseModel = boughtResponses,
                    statusCode = 200
                };
            }
            return new ResponseViewModel()
            {
                message = "Siparişler listelendi. 🥰",
                responseModel = new object(),
                statusCode = 400
            };
        }

        public ResponseViewModel deleteBoughts(string authToken, Guid id)
        {
            authToken = authToken.Replace("Bearer ", string.Empty);
            var stream = authToken;
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jsonToken = handler.ReadJwtToken(stream);
            User user = userService.getUserByUsername(jsonToken.Claims.First().Value);
            Bought bought = boughtRepository.getByUserId(user.id);
            if (user == null)
            {
                return new ResponseViewModel()
                {
                    message = "Kullanıcı doğrulanamadı. 😞",
                    responseModel = new Object(),
                    statusCode = 400
                };
            }
            ResponseViewModel responseViewModel = boughtProductService.deleteByBoughtId(id);
            if (responseViewModel.statusCode == 200)
            {
                bool v = boughtRepository.delete(id);
                if (v)
                {

                    return new ResponseViewModel()
                    {
                        message = "Başarıyla silindi. 🥰",
                        responseModel = new object(),
                        statusCode = 200
                    };
                }
                else
                {
                    return new ResponseViewModel()
                    {
                        message = "Başarıyla silinemedi. 😞",
                        responseModel = new object(),
                        statusCode = 400
                    };
                }
            }
            else
            {
                return new ResponseViewModel()
                {
                    message = "Başarıyla silinemedi. 😞",
                    responseModel = new object(),
                    statusCode = 400
                };
            }


        }

        public ResponseViewModel toggleBoughts(string authToken, Guid id)
        {
            authToken = authToken.Replace("Bearer ", string.Empty);
            var stream = authToken;
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jsonToken = handler.ReadJwtToken(stream);
            User user = userService.getUserByUsername(jsonToken.Claims.First().Value);
            Bought bought = boughtRepository.getByUserId(user.id);
            if (user == null)
            {
                return new ResponseViewModel()
                {
                    message = "Kullanıcı doğrulanamadı. 😞",
                    responseModel = new Object(),
                    statusCode = 400
                };
            }
            Bought bought1 = boughtRepository.get(id);
            if (bought1 != null)
            {
                bought1.archived = !bought1.archived;
                boughtRepository.update(bought1);
                if (bought1.archived)
                {

                    return new ResponseViewModel()
                    {
                        message = "Başarıyla arşivlendi. 🚀",
                        responseModel = new Object(),
                        statusCode = 200
                    };
                }
                else
                {
                    return new ResponseViewModel()
                    {
                        message = "Başarıyla arşivden kaldırıldı. 🚀",
                        responseModel = new Object(),
                        statusCode = 200
                    };
                }

            }
            return new ResponseViewModel()
            {
                message = "Bulunamadı. 😞",
                responseModel = new Object(),
                statusCode = 400
            };


        }


    }
}
