using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.AuthM;
using AmazonClone.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using AmazonClone.Application.ViewModels.CartM;
using AmazonClone.Application.ViewModels.ResponseM;
using AmazonClone.Data.Repositories;
using AmazonClone.Application.ViewModels.AddressM;
using AmazonClone.Domain.Interfaces;
using AmazonClone.Application.ViewModels.CreditCartM;

namespace AmazonClone.Application.Services
{
    public class CreditCartService : ICreditCartService
    {
        private readonly ICreditCartRepository creditCartRepository;
        private readonly IUserService userService;

        public CreditCartService(ICreditCartRepository creditCartRepository, IUserService userService)
        {
            this.creditCartRepository = creditCartRepository;
            this.userService = userService;
        }

        public ResponseViewModel addCreditCart(string authToken,CreditCartAddModel model)
        {
            if (authToken != null)
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
                CreditCart cart = new CreditCart()
                {
                    cartNumber = model.cartNumber,
                    cvvNumber= model.cvvNumber,
                    expDate= model.expDate,
                    nameSurname = model.nameSurname,
                    userId = user.id
                };
                CreditCart cartCreated = creditCartRepository.add(cart);
                return new ResponseViewModel()
                {
                    message = "Kredi kartı başarıyla eklendi. 😍",
                    responseModel = new CreditCartResponseModel()
                    {
                        user= user,
                        nameSurname= cartCreated.nameSurname,
                        expDate= cartCreated.expDate,
                        cvvNumber= cartCreated.cvvNumber,
                        cartNumber = cartCreated.cartNumber
                    },
                    statusCode = 200
                };
            }
            else
            {
                return new ResponseViewModel()
                {
                    statusCode = 400,
                    message = "Token girilmedi. 😒",
                    responseModel = new Object(),
                };
            }
        }

        public ResponseViewModel deleteCreditCart(string authToken,Guid id)
        {
            if (authToken != null)
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
                CreditCart creditCart = creditCartRepository.get(id);
                if (creditCart == null)
                {
                    return new ResponseViewModel()
                    {
                        message = "Kredi kartı bulunamadı. 😐",
                        responseModel = new Object(),
                        statusCode = 400
                    };
                }
                creditCartRepository.delete(id);
                return new ResponseViewModel()
                {
                    message = "Kredi kartı başarıyla silindi. 😍",
                    responseModel = new Object(),
                    statusCode = 200
                };
            }
            else
            {
                return new ResponseViewModel()
                {
                    statusCode = 400,
                    message = "Token girilmedi. 😒",
                    responseModel = new Object(),
                };
            }
        }

        public ResponseViewModel getCreditCarts(string authToken, Guid id)
        {
            if (authToken != null)
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
                List<CreditCart> creditCarts = creditCartRepository.getCartsByUserId(id);
                return new ResponseViewModel()
                {
                    responseModel = creditCarts,
                    message = "Başarıyla getirildi. 🥰",
                    statusCode = 200
                };
                
            }
            else
            {
                return new ResponseViewModel()
                {
                    message = "Kullanıcı doğrulanamadı. 😞",
                    responseModel = new Object(),
                    statusCode = 400
                };
            }
        }

        public ResponseViewModel updateCreditCart(string authToken, CreditCartUpdateModel model)
        {
            if (authToken != null)
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
                CreditCart creditCart = creditCartRepository.get(model.id);
                if (creditCart != null)
                {
                    creditCart.cartNumber = model.cartNumber;
                    creditCart.cvvNumber = model.cvvNumber;
                    creditCart.nameSurname = model.nameSurname;
                    creditCart.expDate = model.expDate;
                    CreditCart cartCreated = creditCartRepository.update(creditCart);
                    return new ResponseViewModel()
                    {
                        message = "Kredi kartı başarıyla eklendi. 😍",
                        responseModel = new CreditCartResponseModel()
                        {
                            user = user,
                            nameSurname = cartCreated.nameSurname,
                            expDate = cartCreated.expDate,
                            cvvNumber = cartCreated.cvvNumber,
                            cartNumber = cartCreated.cartNumber
                        },
                        statusCode = 200
                    };
                }
                else
                {
                    return new ResponseViewModel()
                    {
                        message = "Kredi kartı bulunamadı. 😒",
                        responseModel = new Object(),
                        statusCode = 400
                    };
                }
            }
            else
            {
                return new ResponseViewModel()
                {
                    statusCode = 400,
                    message = "Token girilmedi. 😒",
                    responseModel = new Object(),
                };
            }
        }
    }
}



