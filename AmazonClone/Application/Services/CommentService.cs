using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.CommentM;
using AmazonClone.Application.ViewModels.CommentPhotoM;
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
                    commentPhotos = commentPhotos,
                    userId = model.userId,
                };

                comment = commentRepository.add(comment);
                ICollection<CommentPhotoResponseModel> commentPhotos1 = new List<CommentPhotoResponseModel>();
                foreach (CommentPhoto item in comment.commentPhotos)
                {
                    commentPhotos1.Add(new CommentPhotoResponseModel()
                    {
                        id = item.id,
                        photoUrl = item.photoUrl,
                    });
                }
                return new CommentResponseModel() { 
                    commentPhotos = commentPhotos1,
                    comment = comment.comment,
                    userId = comment.userId,
                };
            }

            return null;
        }

        public CommentResponseModel updateComment(UpdateCommentModel model)
        {
            if (model != null)
            {
                if (commentRepository.get(model.id) != null)
                {
                    Comment comment = commentRepository.get(model.id);
                    comment.comment = model.comment;
                    ICollection<CommentPhoto> commentPhotos = new List<CommentPhoto>();
                    foreach (CreateCommentPhotoModel item in model.commentPhotos)
                    {
                        commentPhotos.Add(new CommentPhoto()
                        {
                            photoUrl = item.photoUrl,
                        });
                    }
                    comment.commentPhotos = commentPhotos;

                    comment = commentRepository.update(comment);

                    ICollection<CommentPhotoResponseModel> commentPhotos1 = new List<CommentPhotoResponseModel>();
                    foreach (CommentPhoto item in comment.commentPhotos)
                    {
                        commentPhotos1.Add(new CommentPhotoResponseModel()
                        {
                            photoUrl = item.photoUrl,
                        });
                    }
                    return new CommentResponseModel()
                    {
                        commentPhotos = commentPhotos1
                    };
                }
                return null;
            }
            return null;
        }

        public bool deleteComment(Guid id)
        {
             return commentRepository.delete(id);
        }

        public CommentResponseModel getComment(Guid id)
        {
            Comment comment = commentRepository.get(id);
            if (comment != null ) { 
                ICollection<CommentPhotoResponseModel> commentPhotos = new List<CommentPhotoResponseModel>();
                foreach (CommentPhoto item in comment.commentPhotos)
                {
                    commentPhotos.Add(new CommentPhotoResponseModel() { 
                        photoUrl = item.photoUrl, id=item.id
                    });
                }
                return new CommentResponseModel()
                {
                    comment = comment.comment,
                    userId = comment.userId,
                    commentPhotos = commentPhotos,
                };
            }
            return null;
        }

    }
}
