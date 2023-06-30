using AmazonClone.Data.Context;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Data.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(BaseContext db) : base(db)
        {
        }

        public Role getRole(string roleName)
        {
            ICollection<Role> roles = dbset.Where(p=>p.name== roleName).ToList();
            if (roles!=null && roles.Any())
            {
                return roles.First();
            }
            return null;
        }
    }
}
