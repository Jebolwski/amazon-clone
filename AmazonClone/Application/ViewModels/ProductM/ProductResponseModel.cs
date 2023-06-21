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
        public static ProductResponseModel convert(Product product)
        {
            HashSet<ProductCategoryResponseModel> productModels = new HashSet<ProductCategoryResponseModel>();



            HashSet<ProductPhotoResponseModel> productPhotoModels = new HashSet<ProductPhotoResponseModel>();
            foreach (ProductPhoto photo in product.photos)
            {
                productPhotoModels.Add(new ProductPhotoResponseModel()
                {
                    photoUrl = photo.photoUrl,
                    id = photo.id
                });
            }

            return new ProductResponseModel()
            {
                id = product.id,
                description = product.description,
                name = product.name,
                price = product.price,
                photos = productPhotoModels,
                productCategories = productModels
            };
        }

    }
}
