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
using AmazonClone.Application.ViewModels.CartM;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AmazonClone.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IProductProductCategoryService productProductCategoryService;

        

        public ProductService(IProductRepository productRepository, 
            IProductProductCategoryService productProductCategoryService)
        {
            this.productRepository = productRepository;
            this.productProductCategoryService = productProductCategoryService;
        }

        public string add(ProductCreateModel model)
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
                
                var obj = new
                {
                    msg = "Successfully added product.",
                    data = new ProductResponseModel()
                    {
                        id = product.id,
                        description = product.description,
                        name = product.name,
                        price = product.price,
                        photos = productPhotoModels,
                        productCategories = productCategories
                    },
                };
                return JsonConvert.SerializeObject(obj);
            }
            return null;
        }

        public CartResponseModel addProductToCart(ProductCreateModel model)
        {
            throw new NotImplementedException();
        }

        public string delete(Guid id)
        {
            //ilk ürün - ürün kategorisi listesinden silinmeli
            //sonra ürün silinmeli
            productProductCategoryService.deleteProductProductCategoriesByProductId(id);
            productRepository.delete(id);
            var obj = new
            {
                msg = "Successfully deleted product.",
            };
            return JsonConvert.SerializeObject(obj);
        }

        public ProductResponseModel get(Guid id)
        {
            Product product = productRepository.getProductWithPhotos(id);
            List<ProductPhotoResponseModel> productPhotoModels = new List<ProductPhotoResponseModel>();
            if (product.photos != null) { 
                foreach (ProductPhoto photo in product.photos)
                {
                    productPhotoModels.Add(new ProductPhotoResponseModel()
                    {
                        photoUrl = photo.photoUrl,
                        id = photo.id
                    });
                }
            }
            if (product != null)
            {
                return new ProductResponseModel()
                {
                    description = product.description,
                    name = product.name,
                    id = product.id,
                    price = product.price,
                    productCategories = productProductCategoryService.getProductCategoriesByProductId(id),
                    photos = productPhotoModels
                };
            }
            return null;
        }

        public string update(ProductUpdateModel model)
        {

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
                id = model.id,
                name = model.name,
                description = model.description,
                price = model.price,
                photos = photos
            };
            product = productRepository.update(product);

            //ilk olan productproductcategoryleri siliyoruz 
            foreach (ProductCategoryResponseModel item in productProductCategoryService.getProductCategoriesByProductId(product.id))
            {
                productProductCategoryService.deleteProductProductCategoriesByProductCategoryId(item.id);
            }
            //şimdi yenileri ekleyeceğiz
            foreach (GuidCreateModel item in model.productCategories)
            {
                productProductCategoryService.add(new ProductProductCategoryCreateModel()
                {
                    productCategoryId = item.id,
                    productId = product.id,
                });
            }

            //artık eklemeler bitti şimdi response oluşturuluyor
            HashSet<ProductCategoryResponseModel> productModels = new HashSet<ProductCategoryResponseModel>();
            HashSet<ProductPhotoResponseModel> productPhotoModels = new HashSet<ProductPhotoResponseModel>();

            foreach (ProductPhoto photo in product.photos)
            {
                productPhotoModels.Add(new ProductPhotoResponseModel()
                {
                    photoUrl = photo.photoUrl,
                    id = photo.id
                });
            }

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

            var obj = new
            {
                msg = "Successfully updated product.",
                data = new ProductResponseModel()
                {
                    id = product.id,
                    description = product.description,
                    name = product.name,
                    price = product.price,
                    photos = productPhotoModels,
                    productCategories = productCategories
                }
            };
            return JsonConvert.SerializeObject(obj);

        }

        public ICollection<ProductResponseModel> filterProductsByName(string productName)
        {
            ICollection<ProductResponseModel> productResponseModels = new List<ProductResponseModel>();
            List<Product> products = productRepository.filterProductsByName(productName);
            if (products != null && products.Any())
            {
                foreach (Product item in products) 
                {
                    productResponseModels.Add(get(item.id));
                }
            }
            return productResponseModels;
        }

    }
}
