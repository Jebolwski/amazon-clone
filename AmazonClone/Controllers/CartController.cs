using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.CartM;
using AmazonClone.Application.ViewModels.CartProductM;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("addTouser")]
        public CartResponseModel addCartToUser(Guid id)
        {
            return cartAppService.addCartToUser(id);
        }

        [HttpPost("add-to-cart"),Authorize("Normal User,Admin")]
        public CartResponseModel add(CartProductCreateModel model)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];

            return cartAppService.addToCart(model, authToken);
        }


    }
}
