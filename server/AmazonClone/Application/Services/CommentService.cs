﻿using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.CommentM;
using AmazonClone.Application.ViewModels.CommentPhotoM;
using AmazonClone.Application.ViewModels.ResponseM;
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

        public ResponseViewModel postComment(PostCommentModel model, string authToken)
        {
            if (model != null)
            {
                if (productService.get(model.productId) == null)
                {
                    return new ResponseViewModel()
                    {
                        message = "Aradığınız ürün bulunamadı.",
                        responseModel = new Object(),
                        statusCode = 400,
                    };
                }

                User user = new User();
                if (authToken != null)
                {
                    authToken = authToken.Replace("Bearer ", string.Empty);
                    var stream = authToken;
                    var handler = new JwtSecurityTokenHandler();
                    JwtSecurityToken jsonToken = handler.ReadJwtToken(stream);
                    user = userService.getUserByUsername(jsonToken.Claims.First().Value);
                    if (user == null)
                    {
                        return new ResponseViewModel()
                        {
                            message = "Yanlış kullanıcı. 😐",
                            responseModel = new Object(),
                            statusCode = 400,
                        };
                    }
                }

                ICollection<CommentPhoto> commentPhotos = new List<CommentPhoto>();
                foreach (CreateCommentPhotoModel item in model.commentPhotos)
                {
                    System.Console.WriteLine(item.photoUrl);
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
                    title = model.title,
                    stars = model.stars
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
                CommentResponseModel commentResponseModel = new CommentResponseModel()
                {
                    commentPhotos = commentPhotos1,
                    comment = comment.comment,
                    userId = comment.userId,
                    productId = comment.productId,
                    stars = comment.stars,
                    title = comment.title
                };
                return new ResponseViewModel()
                {
                    responseModel = commentResponseModel,
                    message = "Mesajınız başarıyla oluşturuldu. 🥰",
                    statusCode = 200
                };
            }
            return new ResponseViewModel()
            {
                message = "Veri verilmedi. 😶",
                responseModel = new Object(),
                statusCode = 400
            };
        }

        public ResponseViewModel updateComment(UpdateCommentModel model, string authToken)
        {
            if (model != null)
            {
                Comment comment = commentRepository.get(model.id);
                if (comment != null)
                {
                    User user = new User();
                    if (authToken != null)
                    {
                        authToken = authToken.Replace("Bearer ", string.Empty);
                        var stream = authToken;
                        var handler = new JwtSecurityTokenHandler();
                        JwtSecurityToken jsonToken = handler.ReadJwtToken(stream);
                        user = userService.getUserByUsername(jsonToken.Claims.First().Value);
                        if (user == null)
                        {
                            return new ResponseViewModel()
                            {
                                message = "Kullanıcı bulunamadı. 😥",
                                responseModel = new Object(),
                                statusCode = 400
                            };
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
                        return new ResponseViewModel()
                        {
                            message = "Yorum başarıyla güncellendi.",
                            responseModel = new CommentResponseModel()
                            {
                                comment = comment.comment,
                                productId = comment.productId,
                                userId = comment.userId,
                                stars = comment.stars,
                                title = comment.title,
                                commentPhotos = commentPhotos1
                            },
                            statusCode = 200
                        };
                    }
                    else
                    {
                        return new ResponseViewModel()
                        {
                            message = "Yorum kullanıcıya ait değil. 😞",
                            responseModel = new Object(),
                            statusCode = 400
                        };
                    }
                }
                return new ResponseViewModel()
                {
                    message = "Yorum kullanıcıya ait değil. 😞",
                    responseModel = new Object(),
                    statusCode = 400
                };
            }
            return new ResponseViewModel()
            {
                message = "Veri verilmedi. 😞",
                responseModel = new Object(),
                statusCode = 400
            };
        }

        public ResponseViewModel deleteComment(Guid id, string authToken)
        {
            User user = new User();
            if (authToken != null)
            {
                authToken = authToken.Replace("Bearer ", string.Empty);
                var stream = authToken;
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jsonToken = handler.ReadJwtToken(stream);
                user = userService.getUserByUsername(jsonToken.Claims.First().Value);
                if (user == null)
                {
                    return new ResponseViewModel()
                    {
                        message = "Kullanıcı doğru değil. 😞",
                        responseModel = new Object(),
                        statusCode = 400
                    };
                }
            }
            Comment comment = commentRepository.get(id);
            if (comment == null)
            {
                return new ResponseViewModel()
                {
                    message = "Yorum bulunamadı. 😞",
                    responseModel = new Object(),
                    statusCode = 400
                };
            }
            if (user.id == comment.userId)
            {
                commentRepository.delete(id);
                return new ResponseViewModel()
                {
                    message = "Ürün başarıyla silindi. 🚀",
                    responseModel = new Object(),
                    statusCode = 200
                };
            }
            return new ResponseViewModel()
            {
                message = "Yorum kullanıcıya ait değil.",
                responseModel = new Object(),
                statusCode = 400
            };
        }

        public ResponseViewModel getComment(Guid id)
        {
            Comment comment = commentRepository.getCommentWithPhotos(id);
            if (comment != null)
            {
                ICollection<CommentPhotoResponseModel> commentPhotos = new List<CommentPhotoResponseModel>();
                foreach (CommentPhoto item in comment.commentPhotos)
                {
                    commentPhotos.Add(new CommentPhotoResponseModel()
                    {
                        photoUrl = item.photoUrl,
                        id = item.id
                    });
                }
                return new ResponseViewModel()
                {
                    message = "Yorum getirildi. 😍",
                    responseModel = new CommentResponseModel()
                    {
                        comment = comment.comment,
                        userId = comment.userId,
                        commentPhotos = commentPhotos,
                        productId = comment.productId,
                        stars = comment.stars,
                        title = comment.title
                    },
                    statusCode = 200
                };
            }
            return new ResponseViewModel()
            {
                message = "Yorum bulunamadı.",
                responseModel = new Object(),
                statusCode = 400
            };
        }

    }
}
