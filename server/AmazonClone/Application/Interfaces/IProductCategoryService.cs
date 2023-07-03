using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Application.ViewModels.ProductCategoryM;

namespace AmazonClone.Application.Interfaces
{
    public interface IProductCategoryService
    {
        public ProductCategoryResponseModel add(ProductCategoryCreateModel model);
        public bool delete(Guid id);
        public ProductCategoryResponseModel update(ProductUpdateModel model);
        public ProductCategoryResponseModel get(Guid id);
        public ICollection<ProductCategoryResponseModel> GetProductCategories();
    }
}
