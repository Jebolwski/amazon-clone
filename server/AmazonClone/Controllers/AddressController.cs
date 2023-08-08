using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.AddressM;
using AmazonClone.Application.ViewModels.AuthM;
using AmazonClone.Application.ViewModels.ResponseM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService AddressService;

        public AddressController(IAddressService AddressService)
        {
            this.AddressService = AddressService;
        }

        [HttpGet("address/{id}"), Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel getAddresses(Guid id)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];
            return AddressService.getAddresses(authToken, id);
        }

        [HttpPost("address/add"), Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel addAddress(AddressAddModel model)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];
            return AddressService.addAddress(authToken, model);
        }

        [HttpDelete("address/{id}/delete"), Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel deleteAddress(Guid id)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];
            return AddressService.deleteAddress(authToken, id);
        }

        [HttpPut("address/update"), Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel update(AddressUpdateModel model)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];

            return AddressService.updateAddress(authToken, model);
        }

        [HttpGet("address-by-id/{id}"), Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel get(Guid id)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];

            return AddressService.getAddressById(authToken, id);
        }
    }
}
