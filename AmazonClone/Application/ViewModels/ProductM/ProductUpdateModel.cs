using AmazonClone.Application.ViewModels.CommentM;
using AmazonClone.Application.ViewModels.ProductCategoryM;
using AmazonClone.Application.ViewModels.ProductPhotoM;
using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.ViewModels.ProductM
{
    public class ProductUpdateModel
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public string description { get; set; }
        public ICollection<PostCommentModel> comments { get; set; }
        public ICollection<ProductCategoryCreateModel> productCategories { get; set; }
        public ICollection<ProductPhotoCreateProduct> photos { get; set; }

        public static Product convert(ProductUpdateModel model)
        {
            ICollection<ProductCategory> productCategories = new HashSet<ProductCategory>();
            foreach (ProductCategoryCreateModel categoryCreateModel in model.productCategories)
            {
                productCategories.Add(new ProductCategory()
                {
                    description = categoryCreateModel.description,
                    name = categoryCreateModel.name,
                });
            }

            ICollection<ProductPhoto> photos = new HashSet<ProductPhoto>();
            foreach (ProductPhotoCreateProduct item in model.photos)
            {
                photos.Add(new ProductPhoto()
                {
                    photoUrl = item.photoUrl,
                });
            }

            ICollection<Comment> comments = new HashSet<Comment>();
            foreach (PostCommentModel item in model.comments)
            {
                comments.Add(PostCommentModel.convert1(item));
            }

            return new Product()
            {
                description = model.description,
                name = model.name,
                price = model.price,
                photos = photos,
                comments = comments,

            };

        }

    }
}
