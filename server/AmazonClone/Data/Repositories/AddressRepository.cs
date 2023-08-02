using AmazonClone.Data.Context;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Data.Repositories
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(BaseContext db) : base(db)
        {
        }

        public List<Address> GetAddressesByUserId(Guid userId)
        {
            return dbset.Where(d => d.userId == userId).ToList();
        }
    }
}
