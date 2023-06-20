using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.CommentM;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Application.ViewModels.ProductCategoryM;
using AmazonClone.Application.ViewModels.ProductPhoto;
using AmazonClone.Data.Repositories;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;
using AmazonClone.Application.ViewModels.ProductPhotoM;

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
                Product product = ProductCreateModel.convert(model);
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
