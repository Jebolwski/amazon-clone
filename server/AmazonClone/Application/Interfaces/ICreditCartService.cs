using AmazonClone.Application.ViewModels.AuthM;
using AmazonClone.Application.ViewModels.CreditCartM;
using AmazonClone.Application.ViewModels.ResponseM;
using AmazonClone.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.Application.Interfaces
{
    public interface ICreditCartService
    {
        public ResponseViewModel getCreditCarts(string authToken, Guid id);
        public ResponseViewModel addCreditCart(string authToken, CreditCartAddModel model);
        public ResponseViewModel updateCreditCart(string authToken, CreditCartUpdateModel model);
        public ResponseViewModel deleteCreditCart(string authToken, Guid id);
    }
}
