using AmazonClone.Application.ViewModels.CommentPhotoM;
using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.ViewModels.CommentM
{
    public class CommentResponseModel
    {
        public Guid id { get; set; }
        public User user { get; set; }
        public string comment { get; set; }
        public ICollection<CommentPhotoResponseModel> commentPhotos { get; set; }
        public Guid productId { get; set; }
        public string title { get; set; }
        public int stars { get; set; }
    }
}
