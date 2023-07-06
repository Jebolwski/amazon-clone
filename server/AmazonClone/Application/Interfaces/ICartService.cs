using AmazonClone.Application.ViewModels.CartM;
using AmazonClone.Application.ViewModels.CartProductM;
using AmazonClone.Application.ViewModels.CommentM;
using AmazonClone.Application.ViewModels.CommentPhotoM;
using AmazonClone.Application.ViewModels.ResponseM;
using AmazonClone.Domain.Entities;
using AmazonClone.Migrations;

namespace AmazonClone.Application.Interfaces
{
    public interface ICartService
    {
        //create new cart for user
        public ResponseViewModel addCartToUser(Guid id);
        public ResponseViewModel addToCart(CartProductCreateModel model, string authToken);
    }
}
