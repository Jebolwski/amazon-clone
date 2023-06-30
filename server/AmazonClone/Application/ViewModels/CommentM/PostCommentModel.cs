using AmazonClone.Application.ViewModels.CommentPhotoM;
using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.ViewModels.CommentM
{
    public class PostCommentModel
    {
        public string comment { get; set; }
        public Guid productId { get; set; }
        public ICollection<CreateCommentPhotoModel> commentPhotos { get; set; }

        public static PostCommentModel convert(Comment comment)
        {
            ICollection<CreateCommentPhotoModel> commentPhotos = new HashSet<CreateCommentPhotoModel>();
            foreach (CommentPhoto commentPhoto in comment.commentPhotos) {
                commentPhotos.Add(new CreateCommentPhotoModel()
                {
                    photoUrl = commentPhoto.photoUrl,
                });
            }
            return new PostCommentModel()
            {
                comment = comment.comment,
                commentPhotos = commentPhotos
            };
        }

        public static Comment convert1(PostCommentModel comment)
        {
            ICollection<CommentPhoto> commentPhotos = new HashSet<CommentPhoto>();
            foreach (CreateCommentPhotoModel commentPhoto in comment.commentPhotos)
            {
                commentPhotos.Add(new CommentPhoto()
                {
                    photoUrl = commentPhoto.photoUrl,
                });
            }
            return new Comment()
            {
                comment= comment.comment,
                commentPhotos = commentPhotos
            };

        }

    }
}
