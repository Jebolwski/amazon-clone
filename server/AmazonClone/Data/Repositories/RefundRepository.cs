using AmazonClone.Data.Context;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Data.Repositories
{
    public class RefundRepository : Repository<Refund>, IRefundRepository
    {
        public RefundRepository(BaseContext db) : base(db)
        {
        }

        public Refund getRefundByBoughtId(Guid id)
        {
            List<Refund> refunds = dbset.Where(x => x.BoughtId == id).ToList();
            if (refunds != null && refunds.Any())
            {
                return refunds[0];
            }
            return null;
        }
    }
}