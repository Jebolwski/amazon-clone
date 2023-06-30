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
    
        public bool delete(Guid id)
        {
            return productProductCategoryRepository.delete(id);
        }

        public ProductProductCategory get(Guid id)
        {
            ProductProductCategory productProductCategory  = productProductCategoryRepository.get(id);
            if (productProductCategory == null) {
                return productProductCategory;
            }
            return null;

        }

        public ICollection<ProductCategoryResponseModel> getProductCategoriesByProductId(Guid productId) {
            ICollection<ProductProductCategory> productProductCategories = productProductCategoryRepository.FindByProductId(productId);
            ICollection<ProductCategoryResponseModel> productCategoryResponseModels = new HashSet<ProductCategoryResponseModel>();
            
            foreach (ProductProductCategory item in productProductCategories)
            {
                productCategoryResponseModels.Add(productCategoryService.get(item.productCategoryId));
            }
            
            return productCategoryResponseModels;
        }

        public bool deleteProductProductCategoriesByProductId(Guid productId)
        {
            List<ProductProductCategory> productProductCategories = productProductCategoryRepository
                .FindByProductId(productId).ToList();
            if (productProductCategories == null)
            {
                return false;
            }
            productProductCategoryRepository.DeleteItems(productProductCategories);
            return true;
        }

        public bool deleteProductProductCategoriesByProductCategoryId(Guid productCategoryId)
        {
            return productProductCategoryRepository.deleteByProductCategoryId(productCategoryId);
        }
    }
}
