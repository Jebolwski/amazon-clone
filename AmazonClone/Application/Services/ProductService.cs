using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.CommentM;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Application.ViewModels.ProductCategoryM;
using AmazonClone.Application.ViewModels.ProductPhotoM;
using AmazonClone.Data.Repositories;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;
using AmazonClone.Application.ViewModels.ProductPhotoM;
using AmazonClone.Application.ViewModels.GuidM;
using AmazonClone.Application.ViewModels.ProductProductCategory;

namespace AmazonClone.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IProductCategoryService productCategoryService;
        private readonly IProductPhotoService productPhotoService;
        private readonly IProductProductCategoryService productProductCategoryService;

        

        public ProductService(IProductRepository productRepository, 
            IProductCategoryService productCategoryService, 
            IProductPhotoService productPhotoService, 
            IProductProductCategoryService productProductCategoryService)
        {
            this.productRepository = productRepository;
            this.productCategoryService = productCategoryService;
            this.productPhotoService = productPhotoService;
            this.productProductCategoryService = productProductCategoryService;
        }

        public ProductResponseModel add(ProductCreateModel model)
        {
            
            if (model != null)
            {
                //fotoğraflar
                ICollection<ProductPhoto> photos = new HashSet<ProductPhoto>();
                foreach (ProductPhotoCreateProduct item in model.photos)
                {
                    photos.Add(new ProductPhoto()
                    {
                        photoUrl = item.photoUrl,
                    });
                }


                Product product = new Product()
                {
                    description = model.description,
                    name = model.name,
                    price = model.price,
                    photos = photos,
                };
                
                //eklendi
                product = productRepository.add(product);

                //response oluşturuluyor

                //ürün kategorileri
                ICollection<ProductCategoryResponseModel> productCategories = new HashSet<ProductCategoryResponseModel>();
                foreach (GuidCreateModel guidCreateModel in model.productCategories)
                {
                    ProductProductCategoryCreateModel productProductCategoryCreateModel = new ProductProductCategoryCreateModel()
                    {
                      productCategoryId = guidCreateModel.id,
                      productId = product.id
                    };

                    productCategories.Add(productProductCategoryService.add(productProductCategoryCreateModel));
                    
                }


                //ürün fotoğrafları
                HashSet<ProductPhotoResponseModel> productPhotoModels = new HashSet<ProductPhotoResponseModel>();
                foreach (ProductPhoto photo in product.photos)
                {
                    productPhotoModels.Add(new ProductPhotoResponseModel()
                    {
                        photoUrl = photo.photoUrl,
                        id = photo.id
                    });
                }

                return new ProductResponseModel()
                {
                    id = product.id,
                    description = product.description,
                    name = product.name,
                    price = product.price,
                    photos = productPhotoModels,
                    productCategories = productCategories
                };
            }
            return null;
        }

        public bool delete(Guid id)
        {
            return productRepository.delete(id);
        }

        public ProductResponseModel get(Guid id)
        {
            Product product = productRepository.get(id);
            if (product != null)
            {
                return ProductResponseModel.convert(product);
            }
            return null;
        }

        public ProductResponseModel update(ProductUpdateModel model)
        {
            Product product = ProductUpdateModel.convert(model);
            product = productRepository.update(product);
            return ProductResponseModel.convert(product);
        }
    }
}
