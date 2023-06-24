using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.AuthM;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public User update(User user)
        {
            if (user != null) { 
                return userRepository.update(user);
            }
            return null;
        }
    }
}
