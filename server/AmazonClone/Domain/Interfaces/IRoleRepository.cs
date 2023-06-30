using AmazonClone.Domain.Entities;

namespace AmazonClone.Domain.Interfaces
{
    public interface IRoleRepository : IRepository<Role>
    {
        public Role getRole(string roleName);
    }
}
