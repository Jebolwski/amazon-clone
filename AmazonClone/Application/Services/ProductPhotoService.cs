using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.Product;
using AmazonClone.Application.ViewModels.ProductCategory;
using AmazonClone.Application.ViewModels.ProductPhoto;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Application.Services
{
    public class ProductPhotoService : IProductPhotoService
    {
        private readonly IProductPhotoRepository productPhotoRepository;

        public ProductPhotoService(IProductPhotoRepository productPhotoRepository)
        {
            this.productPhotoRepository = productPhotoRepository;
        }

        
        public ProductPhotoResponseModel add(ProductPhotoCreateModel model)
        {
            ProductPhoto productPhoto = new ProductPhoto()
            {
                photoUrl = model.photoUrl,
                productId = model.productId,
            };

            ProductPhoto photo = productPhotoRepository.add(productPhoto);
            if (photo != null)
            {
                ProductPhotoResponseModel photoResponseModel = new ProductPhotoResponseModel()
                {
                    photoUrl = photo.photoUrl,
                };
                return photoResponseModel;
            }
            return null;
        }

        public bool delete(Guid id)
        {
            return productPhotoRepository.delete(id);
        }

        public ProductPhotoResponseModel get(Guid id)
        {
            ProductPhoto productPhoto = productPhotoRepository.get(id);
            if (productPhoto != null)
            {
                ProductPhotoResponseModel productPhotoResponseModel = new ProductPhotoResponseModel() { 
                    photoUrl=productPhoto.photoUrl,
                };
                return productPhotoResponseModel;
            }
            return null;
        }

        public ProductPhotoResponseModel update(ProductPhotoUpdateModel model)
        {
            ProductPhoto productPhoto = productPhotoRepository.get(model.id);
            if (productPhoto != null)
            {
                productPhoto.photoUrl = model.photoUrl;
                ProductPhoto productPhoto1 = productPhotoRepository.update(productPhoto);
                ProductPhotoResponseModel responseModel = new ProductPhotoResponseModel()
                {
                    photoUrl = productPhoto1.photoUrl,
                };
                return responseModel;
            }
            return null;
        }
    }
}
