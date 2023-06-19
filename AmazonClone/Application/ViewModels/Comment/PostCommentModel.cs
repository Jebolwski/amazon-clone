using AmazonClone.Application.ViewModels.CommentPhoto;
using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.ViewModels.Comment
{
    public class PostCommentModel
    {
        public Guid userId { get; set; }
        public string comment { get; set; }
        public ICollection<CreateCommentPhotoModel> commentPhotos { get; set; }
    }
}
