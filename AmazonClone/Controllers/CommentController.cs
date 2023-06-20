using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.CommentM;
using AmazonClone.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.Controllers
{
    [Route("api/comment/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentAppService;
        private readonly ICommentRepository commentRepository;

        public CommentController(ICommentService commentAppService, ICommentRepository commentRepository)
        {
            this.commentAppService = commentAppService;
            this.commentRepository = commentRepository;
        }

        [HttpPost("post")]
        public CommentResponseModel postComment(PostCommentModel model)
        {
            return commentAppService.postComment(model);
        }

        [HttpPut("post/update")]
        public CommentResponseModel updateComment(UpdateCommentModel model)
        {
            return commentAppService.updateComment(model);
        }

        [HttpDelete("post/{postId}/delete")]
        public bool deleteComment(Guid postId)
        {
            return commentAppService.deleteComment(postId);
        }

        [HttpGet("post/{postId}")]
        public CommentResponseModel getComment(Guid postId)
        {
            return commentAppService.getComment(postId);
        }
    }
}
