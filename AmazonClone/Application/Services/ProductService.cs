using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.CommentM;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Application.ViewModels.ProductCategoryM;
using AmazonClone.Application.ViewModels.ProductPhotoM;
using AmazonClone.Data.Repositories;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;
using AmazonClone.Application.ViewModels.ProductPhotoM;
using AmazonClone.Application.ViewModels.GuidM;

namespace AmazonClone.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IProductCategoryService productCategoryService;
        private readonly IProductPhotoService productPhotoService;

        public ProductService(IProductRepository productRepository,
            IProductCategoryService productCategoryService,
            IProductPhotoService productPhotoService)
        {
            this.productRepository = productRepository;
            this.productCategoryService = productCategoryService;
            this.productPhotoService = productPhotoService;
        }

        public ProductResponseModel add(ProductCreateModel model)
        {
            
            if (model != null)
            {
                ICollection<ProductCategory> productCategories = new HashSet<ProductCategory>();
                foreach (GuidCreateModel guidCreateModel in model.productCategories)
                {
                    ProductCategoryResponseModel productCategoryResponseModel = productCategoryService.get(guidCreateModel.id);
                    if (productCategoryResponseModel == null)
                    {
                        return null;
                    }
                    productCategories.Add(ProductCategoryResponseModel.convert(productCategoryResponseModel));
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

                Product product = new Product()
                {
                    description = model.description,
                    name = model.name,
                    price = model.price,
                    carts = null,
                    photos = photos,
                    comments = comments,
                    productCategories = productCategories
                };
                product = productRepository.add(product);
                return ProductResponseModel.convert(product);
            }
            return null;
        }

        public bool delete(Guid id)
        {
            return productRepository.delete(id);
        }

        public ProductResponseModel get(Guid id)
        {
            Product product = productRepository.get(id);
            if (product != null)
            {
                return ProductResponseModel.convert(product);
            }
            return null;
        }

        public ProductResponseModel update(ProductUpdateModel model)
        {
            Product product = ProductUpdateModel.convert(model);
            product = productRepository.update(product);
            return ProductResponseModel.convert(product);
        }
    }
}
