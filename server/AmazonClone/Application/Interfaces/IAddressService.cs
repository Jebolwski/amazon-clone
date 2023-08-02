using AmazonClone.Application.ViewModels.AddressM;
using AmazonClone.Application.ViewModels.AuthM;
using AmazonClone.Application.ViewModels.ResponseM;
using AmazonClone.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.Application.Interfaces
{
    public interface IAddressService
    {
        public ResponseViewModel getAddresses(string authToken, Guid id);
        public ResponseViewModel addAddress(string authToken, AddressAddModel model);
        public ResponseViewModel deleteAddress(string authToken, Guid id);
        public ResponseViewModel updateAddress(RefreshTokenModel model);
    }
}
