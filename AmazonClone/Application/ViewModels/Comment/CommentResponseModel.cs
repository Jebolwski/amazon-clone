using AmazonClone.Application.ViewModels.CommentPhoto;

namespace AmazonClone.Application.ViewModels.Comment
{
    public class CommentResponseModel
    {
        public Guid userId { get; set; }
        public string comment { get; set; }
        public ICollection<CommentPhotoResponseModel> commentPhotos { get; set; }
    }
}
