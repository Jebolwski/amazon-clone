using AmazonClone.Data.Context;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Data.Repositories
{
    public class BoughtRepository : Repository<Bought>, IBoughtRepository
    {
        public BoughtRepository(BaseContext db) : base(db)
        {
        }
    }
}
