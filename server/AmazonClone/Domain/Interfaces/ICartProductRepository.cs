using AmazonClone.Domain.Entities;

namespace AmazonClone.Domain.Interfaces
{
    public interface ICartProductRepository : IRepository<CartProduct>
    {
        public ICollection<CartProduct> getByCartId(Guid id);
        public bool deleteByCartIdAndProductId(Guid productId, Guid cartId);
        public bool toggle(Guid cartId, Guid productId);
        public CartProduct getByCartIdAndProductId(Guid cartId, Guid productId);
        public ICollection<CartProduct> getByCartIdStatusOne(Guid id);
        public bool turnOff(Guid cartId, Guid productId);
    }
}
