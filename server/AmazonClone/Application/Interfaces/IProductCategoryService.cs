using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Application.ViewModels.ProductCategoryM;
using AmazonClone.Application.ViewModels.ResponseM;

namespace AmazonClone.Application.Interfaces
{
    public interface IProductCategoryService
    {
        public ResponseViewModel add(ProductCategoryCreateModel model);
        public ResponseViewModel delete(Guid id);
        public ResponseViewModel update(ProductUpdateModel model);
        public ResponseViewModel get(Guid id);
        public ResponseViewModel GetProductCategories();
    }
}
