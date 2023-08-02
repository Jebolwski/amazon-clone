using AmazonClone.Domain.Entities;

namespace AmazonClone.Domain.Interfaces
{
    public interface IAddressRepository : IRepository<Address>
    {
        List<Address> GetAddressesByUserId(Guid userId);
    }
}
