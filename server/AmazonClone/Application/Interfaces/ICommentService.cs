using AmazonClone.Application.ViewModels.CommentM;
using AmazonClone.Application.ViewModels.ResponseM;

namespace AmazonClone.Application.Interfaces
{
    public interface ICommentService
    {
        public CommentResponseModel updateComment(UpdateCommentModel model,string authToken);
        public bool deleteComment(Guid id, string authToken);
        public CommentResponseModel getComment(Guid id);
        public ResponseViewModel postComment(PostCommentModel model, string authToken);
    }
}
