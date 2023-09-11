using AmazonClone.Application.ViewModels.CommentM;
using AmazonClone.Application.ViewModels.ProductCategoryM;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Application.ViewModels.ProductPhotoM;

namespace AmazonClone.Application.ViewModels.CartProduct
{
    public class CartProductProductResponseModel
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public string description { get; set; }
        public ICollection<CommentResponseModel> comments { get; set; }
        public ICollection<ProductCategoryResponseModel> productCategories { get; set; }
        public ICollection<ProductPhotoResponseModel> photos { get; set; }
        public bool status { get; set; }

    }
}