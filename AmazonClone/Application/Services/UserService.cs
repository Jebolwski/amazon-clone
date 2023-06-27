using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.AuthM;
using AmazonClone.Data.Repositories;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User update(User user)
        {
            if (user != null) { 
                return userRepository.update(user);
            }
            return null;
        }

        public User getUserByRefreshToken(string token)
        {
            return userRepository.getUserByRefreshToken(token);
        }

        public User getUserByUsername(string username)
        {
            return userRepository.getUserByUsername(username);
        }

        public User add(User user)
        {
            return userRepository.add(user);
        }
    }
    
}
