using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.ResponseM;
using AmazonClone.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefundController : ControllerBase
    {
        private readonly IRefundService refundService;

        public RefundController(IRefundService refundService)
        {
            this.refundService = refundService;
        }

        [HttpGet("get-code"), Authorize(Roles = "Admin,Normal User")]
        public ResponseViewModel getCode(Guid id)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];
            return refundService.getOrCreateRefundByBoughtId(id, authToken);
        }
    }
}