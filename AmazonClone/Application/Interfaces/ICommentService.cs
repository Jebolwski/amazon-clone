using AmazonClone.Application.ViewModels.Comment;

namespace AmazonClone.Application.Interfaces
{
    public interface ICommentService
    {
        public CommentResponseModel postComment(PostCommentModel model);
    }
}
