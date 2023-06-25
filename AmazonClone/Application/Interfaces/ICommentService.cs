using AmazonClone.Application.ViewModels.CommentM;

namespace AmazonClone.Application.Interfaces
{
    public interface ICommentService
    {
        public CommentResponseModel updateComment(UpdateCommentModel model,string authToken);
        public bool deleteComment(Guid id, string authToken);
        public CommentResponseModel getComment(Guid id);
        public CommentResponseModel postComment(PostCommentModel model, string authToken);
    }
}
