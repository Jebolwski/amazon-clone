using AmazonClone.Domain.Entities;

namespace AmazonClone.Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public User getUserByUsername(string username);

        public User getUserByToken(string token);
    }
}
