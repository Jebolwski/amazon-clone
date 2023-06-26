using AmazonClone.Domain.Entities;

namespace AmazonClone.Domain.Interfaces
{
    public interface ICartProductRepository : IRepository<CartProduct>
    {
        public ICollection<CartProduct> getByCartId(Guid id);
    }
}
