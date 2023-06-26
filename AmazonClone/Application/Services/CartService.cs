using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.CartM;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
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
    }
}
