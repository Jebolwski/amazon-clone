using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
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
          string body =
              """
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
                                  <p>İade isteğiniz alınmıştır.</p>
                                  <p>En yakın MNG kargo şubesine 
                        """
                                            +
                                              random.ToUpper()
                                            +
              """ 
                                  koduyla kargoya veriniz.</p>
                                </div>
                                <div class="bottom" style="text-align: center; font-size: 18px">
                                  Amazon Ekibi
                                </div>
                              </body>
                            </html>
                        """;





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
