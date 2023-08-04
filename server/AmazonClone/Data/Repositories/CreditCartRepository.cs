using AmazonClone.Data.Context;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Data.Repositories
{
    public class CreditCartRepository : Repository<CreditCart>, ICreditCartRepository
    {
        public CreditCartRepository(BaseContext db) : base(db)
        {
        }

        public List<CreditCart> getCartsByUserId(Guid id) {
            return dbset.Where(p => p.userId == id).ToList();
        }


    }
}
