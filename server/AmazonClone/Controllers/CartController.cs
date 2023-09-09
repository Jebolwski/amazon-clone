﻿using AmazonClone.Application.Interfaces;
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

        public CartController(ICartService cartAppService, ICartRepository cartRepository)
        {
            this.cartAppService = cartAppService;
            this.cartRepository = cartRepository;
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

    }
}
