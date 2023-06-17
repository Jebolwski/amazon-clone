using AmazonClone.Application.ViewModels.ProductCategory;
using AmazonClone.Application.ViewModels.ProductPhoto;
using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.ViewModels.Product
{
    public class ProductResponseModel
    {
        public string name { get; set; }
        public float price { get; set; }
        public string description { get; set; }
        public ICollection<ProductCategoryResponseModel> productCategories { get; set; }
        public ICollection<ProductPhotoResponseModel> photos { get; set; }
        public Cart cart { get; set; } = null;
    }
}
