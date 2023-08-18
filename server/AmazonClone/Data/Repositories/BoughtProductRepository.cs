using AmazonClone.Data.Context;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Data.Repositories
{
    public class BoughtProductRepository : Repository<BoughtProduct>, IBoughtProductRespository
    {
        public BoughtProductRepository(BaseContext db) : base(db)
        {
        }
    }
}
