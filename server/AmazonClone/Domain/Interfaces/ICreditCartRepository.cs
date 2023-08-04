using AmazonClone.Domain.Entities;

namespace AmazonClone.Domain.Interfaces
{
    public interface ICreditCartRepository : IRepository<CreditCart>
    {
        public List<CreditCart> getCartsByUserId(Guid id);
    }
}
