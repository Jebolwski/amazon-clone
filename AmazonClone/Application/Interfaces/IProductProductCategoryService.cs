using AmazonClone.Application.ViewModels.ProductCategoryM;
using AmazonClone.Application.ViewModels.ProductProductCategory;
using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.Interfaces
{
    public interface IProductProductCategoryService
    {
        public ProductCategoryResponseModel add(ProductProductCategoryCreateModel model);

        public ICollection<ProductCategoryResponseModel> getProductCategoriesByProductId(Guid productId);

        public bool deleteProductProductCategoriesByProductId(Guid productId);

        public bool deleteProductProductCategoriesByProductCategoryId(Guid productCategoryId);
    }
}
