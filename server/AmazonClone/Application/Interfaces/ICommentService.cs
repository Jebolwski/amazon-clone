using AmazonClone.Application.ViewModels.CommentM;
using AmazonClone.Application.ViewModels.ResponseM;

namespace AmazonClone.Application.Interfaces
{
    public interface ICommentService
    {
        public ResponseViewModel updateComment(UpdateCommentModel model, string authToken);
        public ResponseViewModel deleteComment(Guid id, string authToken);
        public ResponseViewModel getComment(Guid id);
        public ResponseViewModel postComment(PostCommentModel model, string authToken);
    }
}
