using AmazonClone.Application.ViewModels.CommentM;

namespace AmazonClone.Application.Interfaces
{
    public interface ICommentService
    {
        public CommentResponseModel postComment(PostCommentModel model);
        public CommentResponseModel updateComment(UpdateCommentModel model);
        public bool deleteComment(Guid id);
        public CommentResponseModel getComment(Guid id);
    }
}
