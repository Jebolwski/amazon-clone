using AmazonClone.Application.ViewModels.CartM;
using AmazonClone.Application.ViewModels.CommentM;
using AmazonClone.Application.ViewModels.CommentPhotoM;
using AmazonClone.Domain.Entities;
using AmazonClone.Migrations;

namespace AmazonClone.Application.Interfaces
{
    public interface ICartService
    {
        //create new cart for user
        public CartResponseModel addCartToUser(Guid id);
    }
}
