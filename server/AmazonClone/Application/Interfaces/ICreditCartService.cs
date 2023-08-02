using AmazonClone.Application.ViewModels.AuthM;
using AmazonClone.Application.ViewModels.ResponseM;
using AmazonClone.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.Application.Interfaces
{
    public interface ICreditCartService
    {
        public ResponseViewModel getCreditCarts(Guid id);
        public ResponseViewModel addCreditCart(RegisterModel model);
        public ResponseViewModel updateCreditCart(LoginModel request);
        public ResponseViewModel deleteCreditCart(RefreshTokenModel model);
    }
}
