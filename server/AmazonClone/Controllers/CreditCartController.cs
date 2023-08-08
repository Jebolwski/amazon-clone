using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.CreditCartM;
using AmazonClone.Application.ViewModels.ResponseM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCartController : ControllerBase
    {
        private readonly ICreditCartService CreditCartService;

        public CreditCartController(ICreditCartService CreditCartService)
        {
            this.CreditCartService = CreditCartService;
        }

        [HttpGet("creditCart/{id}"), Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel getCreditCarts(Guid id)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];
            return CreditCartService.getCreditCarts(authToken, id);
        }

        [HttpPost("creditCart/add"), Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel addCreditCart(CreditCartAddModel model)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];
            return CreditCartService.addCreditCart(authToken, model);
        }

        [HttpDelete("creditCart/{id}/delete"), Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel deleteCreditCart(Guid id)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];
            return CreditCartService.deleteCreditCart(authToken, id);
        }

        [HttpPut("creditCart/update"), Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel update(CreditCartUpdateModel model)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];

            return CreditCartService.updateCreditCart(authToken, model);
        }
        [HttpGet("creditCart-by-id/{id}"), Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel getById(Guid id)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];

            return CreditCartService.getCreditCartById(authToken, id);
        }

    }
}
