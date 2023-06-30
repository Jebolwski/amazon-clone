using AmazonClone.Application.Interfaces;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public Role get(Guid id)
        {
            return roleRepository.get(id);
        }

        public Role getRole(string roleName)
        {
            return roleRepository.getRole(roleName);
        }
    }
}
