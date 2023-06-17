using AmazonClone.Application.ViewModels.ProductCategory;
using AmazonClone.Application.ViewModels.ProductPhoto;
using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.ViewModels.Product
{
    public class ProductCreateModel
    {
        public string name { get; set; }
        public float price { get; set; }
        public string description { get; set; }
        public ICollection<ProductCategoryCreateModel> productCategories { get; set; }
        public ICollection<ProductPhotoCreateModel> photos { get; set; }
    }
}
