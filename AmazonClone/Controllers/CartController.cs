using AmazonClone.Application.Interfaces;
using AmazonClone.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.Controllers
{
    [Route("api/cart/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService cartAppService;
        private readonly ICartRepository cartRepository;

        public CartController(ICartService cartAppService, ICartRepository cartRepository)
        {
            this.cartAppService = cartAppService;
            this.cartRepository = cartRepository;
        }

    }
}
