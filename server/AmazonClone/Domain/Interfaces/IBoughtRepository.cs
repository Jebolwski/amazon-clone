using AmazonClone.Domain.Entities;

namespace AmazonClone.Domain.Interfaces
{
    public interface IBoughtRepository : IRepository<Bought>
    {
        public bool checkIfThereIs(Guid userId);
        public Bought getByUserId(Guid userId);
        public List<Bought> getAllByUserId(Guid userId);
        public List<Bought> getAllArchivedByUserId(Guid userId);
    }
}
