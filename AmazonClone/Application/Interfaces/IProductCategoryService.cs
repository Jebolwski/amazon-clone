using AmazonClone.Application.ViewModels.Product;
using AmazonClone.Application.ViewModels.ProductCategory;

namespace AmazonClone.Application.Interfaces
{
    public interface IProductCategoryService
    {
        public ProductCategoryResponseModel add(ProductCategoryCreateModel model);
        public bool delete(Guid id);
        public ProductCategoryResponseModel update(ProductUpdateModel model);
        public ProductCategoryResponseModel get(Guid id);
    }
}
