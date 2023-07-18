using AmazonClone.Domain.Entities;

namespace AmazonClone.Domain.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        public Cart getCartByUserId(Guid userId);
    }
}
