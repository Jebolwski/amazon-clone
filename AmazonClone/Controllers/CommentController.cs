﻿using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.CommentM;
using AmazonClone.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.Controllers
{
    [Route("api/comment/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentAppService;
        private readonly IHttpContextAccessor accessor;

        public CommentController(ICommentService commentAppService, IHttpContextAccessor accessor)
        {
            this.commentAppService = commentAppService;
            this.accessor = accessor;
        }

        [HttpPost("post"), Authorize(Roles = "Admin,User")]
        public CommentResponseModel postComment(PostCommentModel model)
        {
            return commentAppService.postComment(model);
        }

        [HttpPut("update")]
        public CommentResponseModel updateComment(UpdateCommentModel model)
        {
            return commentAppService.updateComment(model);
        }

        [HttpDelete("{postId}/delete")]
        public bool deleteComment(Guid postId)
        {
            return commentAppService.deleteComment(postId);
        }

        [HttpGet("{postId}")]
        public CommentResponseModel getComment(Guid postId)
        {
            return commentAppService.getComment(postId);
        }
    }
}
