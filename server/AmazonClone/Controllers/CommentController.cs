using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.CommentM;
using AmazonClone.Application.ViewModels.ResponseM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.Controllers
{
    [Route("api/comment/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentAppService;
        public CommentController(ICommentService commentAppService)
        {
            this.commentAppService = commentAppService;
        }

        [HttpPost("post"), Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel postComment(PostCommentModel model)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];

            return commentAppService.postComment(model, authToken);

        }

        [HttpPut("update"), Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel updateComment(UpdateCommentModel model)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];
            return commentAppService.updateComment(model, authToken);
        }

        [HttpDelete("{commentId}/delete"), Authorize(Roles = "Normal User,Admin")]
        public ResponseViewModel deleteComment(Guid commentId)
        {
            string authToken = HttpContext.Request.Headers["Authorization"];

            return commentAppService.deleteComment(commentId, authToken);
        }

        [HttpGet("{commentId}"), AllowAnonymous]
        public ResponseViewModel getComment(Guid commentId)
        {
            return commentAppService.getComment(commentId);
        }
    }
}
