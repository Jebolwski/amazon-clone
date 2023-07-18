using AmazonClone.Data.Context;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ICartRepository cartRepository;
        public UserRepository(BaseContext db, ICartRepository cartRepository) : base(db)
        {
            this.cartRepository = cartRepository;
        }

        public User getUserByUsername(string username)
        {
            IQueryable<User> users = dbset.Where(p => p.username == username);
            if (users != null && users.Any())
            {
                User user = users.First();
                Cart cart = cartRepository.getCartByUserId(user.id);
                user.cartId = cart.id;
                return user;
            }
            return null;
        }

        public User getUserByRefreshToken(string token)
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
