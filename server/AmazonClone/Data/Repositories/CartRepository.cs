using AmazonClone.Data.Context;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Data.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(BaseContext db) : base(db)
        {
        }

        public Cart getCartByUserId(Guid userId)
        {
            IQueryable<Cart> carts = dbset.Where(x => x.userId == userId);
            if (carts != null && carts.Any())
            {
                return carts.First();
            }
            return null;
        }
    }
}
