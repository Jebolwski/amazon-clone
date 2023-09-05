using System.IdentityModel.Tokens.Jwt;
using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.ResponseM;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.Application.Services
{
    public class RefundService : IRefundService
    {
        private readonly IBoughtRepository boughtRepository;
        private readonly IUserService userService;
        private readonly IRefundRepository refundRepository;

        public RefundService(IBoughtRepository boughtRepository, IUserService userService, IRefundRepository refundRepository)
        {
            this.boughtRepository = boughtRepository;
            this.userService = userService;
            this.refundRepository = refundRepository;
        }

        public ResponseViewModel getOrCreateRefundByBoughtId(Guid id, string authToken)
        {
            authToken = authToken.Replace("Bearer ", string.Empty);
            var stream = authToken;
            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jsonToken = handler.ReadJwtToken(stream);
            User user = userService.getUserByUsername(jsonToken.Claims.First().Value);
            Bought bought = boughtRepository.get(id);
            if (user.id == bought.userId)
            {
                Refund refund2 = refundRepository.getRefundByBoughtId(id);
                if (refund2 == null)
                {
                    Random ran = new Random();

                    String b = "abcdefghijklmnopqrstuvwxyz0123456789";
                    String sc = "!@#$%^&*~";

                    int length = 6;

                    String random = "";

                    for (int i = 0; i < length; i++)
                    {
                        int a = ran.Next(b.Length); //string.Lenght gets the size of string
                        random = random + b.ElementAt(a);
                    }
                    for (int j = 0; j < 2; j++)
                    {
                        int sz = ran.Next(sc.Length);
                        random = random + sc.ElementAt(sz);
                    }
                    Refund refund = new Refund()
                    {
                        BoughtId = id,
                        RefundCode = random,
                    };
                    Refund refund1 = refundRepository.add(refund);
                    if (refund1 != null)
                    {
                        return new ResponseViewModel()
                        {
                            message = "Başarıyla getirildi. 🚀",
                            responseModel = refund1,
                            statusCode = 200
                        };
                    }
                    else
                    {
                        return new ResponseViewModel()
                        {
                            message = "Kod başarıyla oluşturulamadı. ",
                            responseModel = new object(),
                            statusCode = 400
                        };
                    }
                }
                else
                {
                    return new ResponseViewModel()
                    {
                        message = "Başarıyla getirildi. 🚀",
                        responseModel = refund2,
                        statusCode = 200
                    };
                }
            }
            else
            {
                return new ResponseViewModel()
                {
                    message = "Kullanıcı doğrulanamadı. 😐",
                    responseModel = new object(),
                    statusCode = 400
                };
            }
        }
    }
}
