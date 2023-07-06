using AmazonClone.Application.ViewModels.ProductCategoryM;
using AmazonClone.Application.ViewModels.ProductProductCategory;
using AmazonClone.Application.ViewModels.ResponseM;
using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.Interfaces
{
    public interface IProductProductCategoryService
    {
        public ResponseViewModel add(ProductProductCategoryCreateModel model);

        public ResponseViewModel getProductCategoriesByProductId(Guid productId);

        public ResponseViewModel deleteProductProductCategoriesByProductId(Guid productId);

        public bool deleteProductProductCategoriesByProductCategoryId(Guid productCategoryId);
    }
}
