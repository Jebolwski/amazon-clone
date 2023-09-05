using AmazonClone.Application.ViewModels.CartM;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Application.ViewModels.ResponseM;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.Application.Interfaces
{
    public interface IRefundService
    {
        public ResponseViewModel getOrCreateRefundByBoughtId(Guid id, string authToken);

    }
}
