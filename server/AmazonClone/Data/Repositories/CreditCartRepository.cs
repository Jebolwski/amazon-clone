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


    }
}
