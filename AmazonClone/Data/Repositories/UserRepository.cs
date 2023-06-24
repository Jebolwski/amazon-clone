using AmazonClone.Data.Context;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BaseContext db) : base(db)
        {
        }

        public User getUserByUsername(string username)
        {
            IQueryable<User> users = dbset.Where(p => p.username == username);
            if (users!=null && users.Any())
            {
                return users.First();
            }
            return null;
        }

        public User getUserByToken(string token)
        {
            IQueryable<User> users = dbset.Where(p => p.RefreshToken == token);
            if (users != null && users.Any())
            {
                return users.First();
            }
            return null;
        }
    }
}
