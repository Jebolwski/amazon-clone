using AmazonClone.Application.ViewModels.ProductPhotoM;
using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.Interfaces
{
    public interface IProductPhotoService
    {
        public ProductPhotoResponseModel add(ProductPhotoCreateModel model);
        public bool delete(Guid id);
        public ProductPhotoResponseModel update(ProductPhotoUpdateModel model);
        public ProductPhotoResponseModel get(Guid id);
    }
}
