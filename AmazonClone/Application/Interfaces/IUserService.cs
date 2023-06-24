using AmazonClone.Application.ViewModels.AuthM;
using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.Interfaces
{
    public interface IUserService
    {
        public User update(User user);
    }
}
