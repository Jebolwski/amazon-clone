using AmazonClone.Application.ViewModels.CartM;
using AmazonClone.Application.ViewModels.CartProductM;
using AmazonClone.Application.ViewModels.CommentM;
using AmazonClone.Application.ViewModels.CommentPhotoM;
using AmazonClone.Application.ViewModels.ResponseM;
using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.Interfaces
{
    public interface ICartService
    {
        //create new cart for user
        public ResponseViewModel addCartToUser(Guid id);
        public ResponseViewModel addToCart(CartProductCreateModel model, string authToken);
        public ResponseViewModel getCart(string authToken);
        public ResponseViewModel deleteProductFromCart(string authToken, Guid productId, Guid cartId);
        public ResponseViewModel buyTheCart(string authToken, Guid cartId);
        public ResponseViewModel buyTheCartNow(string authToken, Guid cartId, Guid productId);
        public ResponseViewModel getCartByUserId(Guid userId);

    }
}
