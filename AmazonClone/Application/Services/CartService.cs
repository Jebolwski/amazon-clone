using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.CartM;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Application.ViewModels.CartProductM;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository cartRepository;
        private readonly ICartProductService cartProductService;

        public CartService(ICartRepository cartRepository, ICartProductService cartProductService)
        {
            this.cartRepository = cartRepository;
            this.cartProductService = cartProductService;
        }

        public CartResponseModel addCartToUser(Guid id)
        {
            if (id != null)
            {
                Cart cart = cartRepository.add(new Cart()
                {
                    userId = id,
                });

                return new CartResponseModel()
                {
                    id = cart.id,
                    userId = id,
                    products = new HashSet<ProductResponseModel>()
                };
            }
            return null;
        }
    
        public CartResponseModel addToCart(CartProductCreateModel model,string authToken)
        {
            return cartProductService.add(model);
        }
    }
}
