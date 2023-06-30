using AmazonClone.Application.ViewModels.ProductCategoryM;
using AmazonClone.Application.ViewModels.ProductPhotoM;
using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.ViewModels.ProductM
{
    public class ProductResponseModel
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public string description { get; set; }
        public ICollection<ProductCategoryResponseModel> productCategories { get; set; }
        public ICollection<ProductPhotoResponseModel> photos { get; set; }

    }
}
