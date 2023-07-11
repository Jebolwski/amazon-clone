using AmazonClone.Application.ViewModels.CommentPhotoM;

namespace AmazonClone.Application.ViewModels.CommentM
{
    public class UpdateCommentModel
    {
        public Guid id { get; set; }
        public string comment { get; set; }
        public string title { get; set; }
        public int stars { get; set; }
        public ICollection<CreateCommentPhotoModel> commentPhotos { get; set; }
    }
}