using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.BoughtProductM;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Application.ViewModels.ResponseM;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace AmazonClone.Application.Services
{
    public class BoughtService : IBoughtService
    {
        private readonly ICartService cartService;
        private readonly IBoughtProductService boughtProductService;
        private readonly IBoughtRepository boughtRepository;
        private readonly IUserService userService;

        public BoughtService(ICartService cartService, IBoughtProductService boughtProductService, IBoughtRepository boughtRepository, IUserService userService)
        {
            this.cartService = cartService;
            this.boughtProductService = boughtProductService;
            this.boughtRepository = boughtRepository;
            this.userService = userService;
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
            ResponseViewModel responseViewModel = cartService.getCart(authToken);
            Bought boughtCreated = boughtRepository.add(new Bought()
            {
                userId = user.id,
                timeBought = DateTime.UtcNow,
            });
            object responseModel = responseViewModel.responseModel;
            ICollection <ProductResponseModel> products = (ICollection<ProductResponseModel>)responseModel.products;
            foreach (ProductResponseModel product in products)
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

            return new ResponseViewModel()
            {
                message = "Önceden alınanlara başarıyla eklendi. 🥰",
                responseModel= new object(),
                statusCode = 200
            };
        }
    }
}
