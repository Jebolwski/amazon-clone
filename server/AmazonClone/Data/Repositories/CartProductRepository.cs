using AmazonClone.Data.Context;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Data.Repositories
{
    public class CartProductRepository : Repository<CartProduct>, ICartProductRepository
    {
        public CartProductRepository(BaseContext db) : base(db)
        {
        }

        public ICollection<CartProduct> getByCartId(Guid id)
        {
            return dbset.Where(p => p.cartId == id).ToList();
        }
    }
}
