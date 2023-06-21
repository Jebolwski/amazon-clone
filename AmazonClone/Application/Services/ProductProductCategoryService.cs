using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.ProductCategoryM;
using AmazonClone.Application.ViewModels.ProductProductCategory;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Application.Services
{
    public class ProductProductCategoryService : IProductProductCategoryService
    {
        private readonly IProductProductCategoryRepository productProductCategoryRepository;
        private readonly IProductCategoryService productCategoryService;

        public ProductProductCategoryService(IProductProductCategoryRepository productProductCategoryRepository,
            IProductCategoryService productCategoryService)
        {
            this.productProductCategoryRepository = productProductCategoryRepository;
            this.productCategoryService = productCategoryService;
        }

        public ProductCategoryResponseModel add(ProductProductCategoryCreateModel model)
        {
            ProductCategoryResponseModel productCategoryResponseModel = productCategoryService.get(model.productCategoryId);
            if (productCategoryResponseModel == null)
            {
                return null;
            }
            ProductProductCategory productProductCtg = productProductCategoryRepository.add(new ProductProductCategory()
            {
                productCategoryId = model.productCategoryId,
                productId = model.productId,
                
            });
            return productCategoryResponseModel;
        }
    }
}
