using AmazonClone.Application.ViewModels.Product;

namespace AmazonClone.Application.Interfaces
{
    public interface IProductService
    {
        public ProductResponseModel get(Guid id);
        public bool delete(Guid id);
        public ProductResponseModel update(ProductUpdateModel model);
        public ProductResponseModel add(ProductCreateModel model);

    }
}
