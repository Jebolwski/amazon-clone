using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.Product;
using AmazonClone.Application.ViewModels.ProductCategory;
using AmazonClone.Application.ViewModels.ProductPhoto;
using AmazonClone.Data.Repositories;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

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
                    photos.Add(new ProductPhoto() { 
                        photoUrl = item.photoUrl,
                    });
                }

                Product product = new Product()
                {
                    description = model.description,
                    name = model.name,
                    price = model.price,
                    carts = null,
                    photos = photos,
                    productCategories = productCategories

                };
                product = productRepository.add(product);

                HashSet<ProductCategoryResponseModel> productModels = new HashSet<ProductCategoryResponseModel>();
                foreach (ProductCategory category in product.productCategories)
                {
                    productModels.Add(new ProductCategoryResponseModel() { 
                        description = category.description,
                        name = category.name,
                        id = category.id
                    });
                }

                HashSet<ProductPhotoResponseModel> productPhotoModels = new HashSet<ProductPhotoResponseModel>();
                foreach (ProductPhoto photo in product.photos)
                {
                    productPhotoModels.Add(new ProductPhotoResponseModel()
                    {
                        photoUrl = photo.photoUrl,
                        id = photo.id
                    });
                }
                
                ProductResponseModel productResponse = new ProductResponseModel()
                {
                    id = product.id,
                    description = product.description,
                    name = product.name,
                    price = product.price,
                    cart = null,
                    photos = productPhotoModels,
                    productCategories = productModels
                };
                return productResponse;
            }
            return null;
        }

        public bool delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public ProductResponseModel get(Guid id)
        {
            throw new NotImplementedException();
        }

        public ProductResponseModel update(ProductUpdateModel model)
        {
            throw new NotImplementedException();
        }
    }
}
