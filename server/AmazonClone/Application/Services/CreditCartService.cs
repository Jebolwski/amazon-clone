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

namespace AmazonClone.Application.Services
{
    public class CreditCartService : ICreditCartService
    {
        public ResponseViewModel addCreditCart(RegisterModel model)
        {
            throw new NotImplementedException();
        }

        public ResponseViewModel deleteCreditCart(RefreshTokenModel model)
        {
            throw new NotImplementedException();
        }

        public ResponseViewModel getCreditCarts(Guid id)
        {
            throw new NotImplementedException();
        }

        public ResponseViewModel updateCreditCart(LoginModel request)
        {
            throw new NotImplementedException();
        }
    }
}



