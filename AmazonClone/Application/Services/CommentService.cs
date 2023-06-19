using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.Comment;
using AmazonClone.Application.ViewModels.CommentPhoto;
using AmazonClone.Data.Repositories;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public CommentResponseModel postComment(PostCommentModel model)
        {
            if (model != null)
            {
                ICollection<CommentPhoto> commentPhotos = new List<CommentPhoto>();
                foreach (CreateCommentPhotoModel item in model.commentPhotos)
                {
                    commentPhotos.Add(new CommentPhoto()
                    {
                        photoUrl = item.photoUrl,
                    });
                }

                Comment comment = new Comment()
                {
                    comment = model.comment,
                    commentPhotos = commentPhotos
                };

                comment = commentRepository.add(comment);
                ICollection<CommentPhotoResponseModel> commentPhotos1 = new List<CommentPhotoResponseModel>();
                foreach (CommentPhoto item in comment.commentPhotos)
                {
                    commentPhotos1.Add(new CommentPhotoResponseModel()
                    {
                        photoUrl = item.photoUrl,
                    });
                }
                return new CommentResponseModel() { 
                    commentPhotos=commentPhotos1
                };

            }
            return null;
        }

    }
}
