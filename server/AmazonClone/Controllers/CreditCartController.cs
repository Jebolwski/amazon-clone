using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.CreditCartM;
using AmazonClone.Application.ViewModels.ResponseM;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;

namespace AmazonClone.Controllers
{
    [System.Web.Http.Route("api/[controller]")]
    [ApiController]
    public class CreditCartController : Controller
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
    }
}
