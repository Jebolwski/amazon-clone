using AmazonClone.Application.ViewModels.ProductCategoryM;
using AmazonClone.Application.ViewModels.ProductProductCategory;
using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.Interfaces
{
    public interface IProductProductCategoryService
    {
        public ProductCategoryResponseModel add(ProductProductCategoryCreateModel model);
    }
}
