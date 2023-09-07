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

        [HttpPost("add-bought"), Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel addBought()
        {
            string authToken = HttpContext.Request.Headers["Authorization"];
            return boughtService.addBought(authToken);
        }

        [HttpGet("all-bought"), Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel getBoughts()
        {
            string authToken = HttpContext.Request.Headers["Authorization"];
            return boughtService.getBoughts(authToken);
        }

        [HttpGet("all-bought-archived"), Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel getArchivedBoughts()
        {
            string authToken = HttpContext.Request.Headers["Authorization"];
            return boughtService.getArchivedBoughts(authToken);
        }

        [HttpDelete("delete-bought/{id}"), Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel getBoughts(Guid id)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];
            return boughtService.deleteBoughts(authToken, id);
        }
    }
}
