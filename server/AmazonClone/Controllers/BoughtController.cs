using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.ResponseM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoughtController : ControllerBase
    {
        private readonly IBoughtService boughtService;

        public BoughtController(IBoughtService boughtService)
        {
            this.boughtService = boughtService;
        }

        [HttpPost, Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel addBought()
        {
            string authToken = HttpContext.Request.Headers["Authorization"];
            return boughtService.addBought(authToken);
        }

        [HttpGet, Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel getBoughts()
        {
            string authToken = HttpContext.Request.Headers["Authorization"];
            return boughtService.getBoughts(authToken);
        }
    }
}
