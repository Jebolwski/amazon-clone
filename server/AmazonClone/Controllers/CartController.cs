using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.CartM;
using AmazonClone.Application.ViewModels.CartProductM;
using AmazonClone.Application.ViewModels.ResponseM;
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
        private readonly ICartProductService cartProductService;

        public CartController(ICartService cartAppService, ICartRepository cartRepository, ICartProductService cartProductService)
        {
            this.cartAppService = cartAppService;
            this.cartRepository = cartRepository;
            this.cartProductService = cartProductService;
        }

        [HttpPost("add-to-user")]
        public ResponseViewModel addCartToUser(Guid id)
        {
            return cartAppService.addCartToUser(id);
        }

        [HttpPost("add-to-cart"), Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel add(CartProductCreateModel model)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];

            return cartAppService.addToCart(model, authToken);
        }
        [HttpGet("cart/{id}"), Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel get(Guid id)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];

            return cartAppService.getCart(authToken);
        }

        [HttpDelete("{productId}/{cartId}"), Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel DeleteProductFromCart(Guid cartId, Guid productId)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];

            return cartAppService.deleteProductFromCart(authToken, productId, cartId);
        }

        [HttpPost("{cartId}/buy"), Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel BuyCart(Guid cartId)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];

            return cartAppService.buyTheCart(authToken, cartId);
        }

        [HttpPost("{cartId}/{productId}/buy-now"), Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel BuyCartNow(Guid cartId, Guid productId)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];

            return cartAppService.buyTheCartNow(authToken, cartId, productId);
        }

        [HttpPost("{cartId}/{productId}/toggle-status"), Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel toggleStatus(Guid cartId, Guid productId)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];

            return cartProductService.toggleStatus(cartId, productId);
        }

        [HttpPost("{cartId}/toggle-off"), Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel toggleOff(Guid cartId)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];

            return cartProductService.toggleAllStatusOff(cartId, authToken);
        }

    }
}
