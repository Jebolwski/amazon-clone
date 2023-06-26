using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.Interfaces
{
    public interface IRoleService
    {
        public Role get(Guid id);
        public Role getRole(string roleName);
    }
}
