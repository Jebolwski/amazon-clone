using AmazonClone.Domain.Entities;

namespace AmazonClone.Domain.Interfaces
{
    public interface IBoughtRepository : IRepository<Bought>
    {
        public bool checkIfThereIs(Guid userId);
        public Bought getByUserId(Guid userId);
        public Bought getAllByUserId(Guid userId);
    }
}
