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

        public ICollection<CartProduct> getByCartIdStatusOne(Guid id)
        {
            return dbset.Where(p => p.cartId == id).Where(p => p.status == true).ToList();
        }

        public bool deleteByCartIdAndProductId(Guid productId, Guid cartId)
        {
            List<CartProduct> cartProducts = dbset.Where(p => p.cartId == cartId).Where(p => p.productId == productId).ToList();
            if (cartProducts != null && cartProducts.Any())
            {
                CartProduct cartProduct = cartProducts.First();
                delete(cartProduct.id);
                return true;
            }
            return false;
        }

        public bool toggle(Guid cartId, Guid productId)
        {
            List<CartProduct> cartProducts = dbset.Where(p => p.cartId == cartId).Where(p => p.productId == productId).ToList();
            if (cartProducts != null && cartProducts.Any())
            {
                bool status = false;
                foreach (CartProduct cartProduct in cartProducts)
                {
                    cartProduct.status = !cartProduct.status;
                    status = cartProduct.status;
                    db.Update(cartProduct);
                    db.SaveChanges();
                }
                return status;
            }
            return false;
        }

        public bool turnOff(Guid cartId, Guid productId)
        {
            List<CartProduct> cartProducts = dbset.Where(p => p.cartId == cartId).Where(p => p.productId == productId).ToList();
            if (cartProducts != null && cartProducts.Any())
            {
                foreach (CartProduct cartProduct in cartProducts)
                {
                    cartProduct.status = false;
                    db.Update(cartProduct);
                    db.SaveChanges();
                }
                return true;
            }
            return false;
        }

        public CartProduct getByCartIdAndProductId(Guid cartId, Guid productId)
        {
            List<CartProduct> cartProducts = dbset.Where(p => p.cartId == cartId).Where(p => p.productId == productId).ToList();
            if (cartProducts != null && cartProducts.Any())
            {
                return cartProducts.First();
            }
            return null;
        }
    }
}
