using AmazonClone.Domain.Entities;

namespace AmazonClone.Domain.Interfaces
{
    public interface IRefundRepository : IRepository<Refund>
    {
        Refund getRefundByBoughtId(Guid id);
    }
}
