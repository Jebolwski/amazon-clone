using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.CommentM;
using AmazonClone.Application.ViewModels.CommentPhotoM;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace AmazonClone.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;
        private readonly IUserService userService;
        private readonly IProductService productService;

        public CommentService(ICommentRepository commentRepository, IUserService userService, IProductService productService)
        {
            this.commentRepository = commentRepository;
            this.userService = userService;
            this.productService = productService;
        }

        public CommentResponseModel postComment(PostCommentModel model,string authToken)
        {
            if (model != null)
            {
                if (productService.get(model.productId) == null)
                {
                    return null;    
                }
                
                User user = null;
                if (authToken != null)
                {
                    authToken = authToken.Replace("Bearer ", string.Empty);
                    var stream = authToken;
                    var handler = new JwtSecurityTokenHandler();
                    JwtSecurityToken jsonToken = handler.ReadJwtToken(stream);
                    user = userService.getUserByUsername(jsonToken.Claims.First().Value);
                    if (user == null)
                    {
                        return null;
                    }
                }

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
                    userId = user.id,
                    commentPhotos = commentPhotos,
                    productId = model.productId,
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

        public CommentResponseModel updateComment(UpdateCommentModel model,string authToken)
        {
            if (model != null)
            {
                Comment comment = commentRepository.get(model.id);
                if (comment!=null)
                {
                    User user = null;
                    if (authToken != null)
                    {
                        authToken = authToken.Replace("Bearer ", string.Empty);
                        var stream = authToken;
                        var handler = new JwtSecurityTokenHandler();
                        JwtSecurityToken jsonToken = handler.ReadJwtToken(stream);
                        user = userService.getUserByUsername(jsonToken.Claims.First().Value);
                        if (user == null)
                        {
                            return null;
                        }
                    }
                    if (user.id == comment.userId)
                    {
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
                                id = item.id,
                            });
                        }
                        return new CommentResponseModel()
                        {
                            comment = comment.comment,
                            productId = comment.productId,
                            userId = comment.userId,
                            commentPhotos = commentPhotos1
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            return null;
        }

        public bool deleteComment(Guid id,string authToken)
        {
            User user = null;
            if (authToken != null)
            {
                authToken = authToken.Replace("Bearer ", string.Empty);
                var stream = authToken;
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jsonToken = handler.ReadJwtToken(stream);
                user = userService.getUserByUsername(jsonToken.Claims.First().Value);
                if (user == null)
                {
                    return false;
                }
            }
            Comment comment = commentRepository.get(id);
            if (comment != null && user.id == comment.userId)
            {
                return commentRepository.delete(id);
            }
            return false;
        }

        public CommentResponseModel getComment(Guid id)
        {
            Comment comment = commentRepository.getCommentWithPhotos(id);
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
                    productId = comment.productId,
                };
            }
            return null;
        }

    }
}
