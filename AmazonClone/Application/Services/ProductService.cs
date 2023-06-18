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
                

                Product product = new Product()
                {
                    description = model.description,
                    name = model.name,
                    price = model.price,
                    carts = null,
                };
                product = productRepository.add(product);

                //veritabanına ürün kategorileri ekleniyor
                HashSet<ProductCategoryResponseModel> productModels = new HashSet<ProductCategoryResponseModel>();
                foreach (ProductCategoryCreateModel category in model.productCategories)
                {
                    productModels.Add(productCategoryService.add(category));

                }

                //veritabanına ürün fotoğrafları ekleniyor
                HashSet<ProductPhotoResponseModel> productPhotoModels = new HashSet<ProductPhotoResponseModel>();
                foreach (ProductPhotoCreateProduct photo in model.photos)
                {
                    ProductPhotoCreateModel productPhotoCreateModel = new ProductPhotoCreateModel()
                    {
                        productId = product.Id,
                        photoUrl = photo.photoUrl,
                    };
                    productPhotoModels.Add(productPhotoService.add(productPhotoCreateModel));
                }
                
                ProductResponseModel productResponse = new ProductResponseModel()
                {
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
