using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.CommentM;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Application.ViewModels.ProductCategoryM;
using AmazonClone.Application.ViewModels.ProductPhotoM;
using AmazonClone.Data.Repositories;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;
using AmazonClone.Application.ViewModels.GuidM;
using AmazonClone.Application.ViewModels.ProductProductCategory;
using AmazonClone.Application.ViewModels.CartM;
using Newtonsoft.Json;
using AmazonClone.Application.ViewModels.ResponseM;
using AmazonClone.Application.ViewModels.CommentPhotoM;

namespace AmazonClone.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IProductPhotoService productPhotoService;
        private readonly IProductProductCategoryService productProductCategoryService;
        private readonly IProductProductCategoryRepository productProductCategoryRespository;
        private readonly IUserService userService;


        public ProductService(IProductRepository productRepository,
            IProductProductCategoryService productProductCategoryService,
            IProductPhotoService productPhotoService,
            IProductProductCategoryRepository productProductCategoryRespository,
            IUserService userService)
        {
            this.productRepository = productRepository;
            this.productProductCategoryService = productProductCategoryService;
            this.productPhotoService = productPhotoService;
            this.productProductCategoryRespository = productProductCategoryRespository;
            this.userService = userService;
        }

        public ResponseViewModel add(ProductCreateModel model)
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

                    productCategories.Add((ProductCategoryResponseModel)productProductCategoryService.add(productProductCategoryCreateModel).responseModel);

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

                ProductResponseModel productResponseModel = new ProductResponseModel()
                {
                    id = product.id,
                    description = product.description,
                    name = product.name,
                    price = product.price,
                    photos = productPhotoModels,
                    productCategories = productCategories
                };

                return new ResponseViewModel()
                {
                    message = "Ürün başarıyla eklendi. 🚀",
                    responseModel = productResponseModel,
                    statusCode = 200,
                };
            }
            return new ResponseViewModel()
            {
                responseModel = new Object(),
                statusCode = 400,
                message = "Model girilmedi. 😞"
            };
        }



        public ResponseViewModel delete(Guid id)
        {
            //ilk ürün - ürün kategorisi listesinden silinmeli
            //sonra ürün silinmeli
            Product product = productRepository.get(id);
            if (product == null)
            {
                return new ResponseViewModel()
                {
                    message = "Ürün bulunamadı. 😢",
                    statusCode = 400,
                    responseModel = new Object()
                };
            }
            productProductCategoryService.deleteProductProductCategoriesByProductId(id);
            productRepository.delete(id);
            return new ResponseViewModel()
            {
                message = "Başarıyla ürün silindi. 🥰",
                statusCode = 200,
                responseModel = new Object()
            };
        }

        public ResponseViewModel get(Guid id)
        {
            Product product = productRepository.getProductWithPhotos(id);
            if (product != null)
            {
                List<ProductPhotoResponseModel> productPhotoModels = new List<ProductPhotoResponseModel>();
                if (product.photos != null)
                {
                    foreach (ProductPhoto photo in product.photos)
                    {
                        productPhotoModels.Add(new ProductPhotoResponseModel()
                        {
                            photoUrl = photo.photoUrl,
                            id = photo.id
                        });
                    }
                }
                List<CommentResponseModel> comments = new List<CommentResponseModel>();
                if (product.comments != null)
                {
                    foreach (Comment comment in product.comments)
                    {
                        List<CommentPhotoResponseModel> photos = new List<CommentPhotoResponseModel>();
                        System.Console.WriteLine(comment.commentPhotos.Any());
                        foreach (var photo in comment.commentPhotos)
                        {
                            photos.Add(new CommentPhotoResponseModel()
                            {
                                id = photo.id,
                                photoUrl = photo.photoUrl
                            });
                        }
                        comments.Add(new CommentResponseModel()
                        {
                            id = comment.id,
                            comment = comment.comment,
                            productId = comment.productId,
                            stars = comment.stars,
                            title = comment.title,
                            user = userService.get(comment.userId),
                            commentPhotos = photos
                        });
                    }
                }
                ICollection<ProductCategoryResponseModel> productCategories = (HashSet<ProductCategoryResponseModel>)productProductCategoryService
                        .getProductCategoriesByProductId(id).responseModel;
                return new ResponseViewModel()
                {
                    message = "Ürün başarıyla getirildi. ✨",
                    statusCode = 200,
                    responseModel = new ProductResponseModel()
                    {
                        description = product.description,
                        name = product.name,
                        id = product.id,
                        price = product.price,
                        productCategories = productCategories,
                        photos = productPhotoModels,
                        comments = comments
                    }
                };
            }
            return new ResponseViewModel()
            {
                message = "Ürün bulunamadı. 😐",
                statusCode = 400,
                responseModel = new Object()
            };
        }

        public ResponseViewModel update(ProductUpdateModel model)
        {
            ProductResponseModel responsemodel = (ProductResponseModel)this.get(model.id).responseModel;
            foreach (ProductPhotoResponseModel photo in responsemodel.photos)
            {
                productPhotoService.delete(photo.id);
            }

            ICollection<ProductPhoto> photos = new HashSet<ProductPhoto>();
            foreach (ProductPhotoCreateProduct item in model.photos)
            {
                photos.Add(new ProductPhoto()
                {
                    photoUrl = item.photoUrl,
                });
            };




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
            productProductCategoryService.deleteProductProductCategoriesByProductId(product.id);

            //şimdi yenileri ekleyeceğiz
            ICollection<ProductCategoryResponseModel> productCategories = new HashSet<ProductCategoryResponseModel>();
            foreach (GuidCreateModel item in model.productCategories)
            {
                ResponseViewModel responseViewModel = productProductCategoryService.add(new ProductProductCategoryCreateModel()
                {
                    productCategoryId = item.id,
                    productId = product.id,
                });
                if (responseViewModel != null && responseViewModel.responseModel != null)
                {
                    productCategories.Add((ProductCategoryResponseModel)responseViewModel.responseModel);
                }
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

            return new ResponseViewModel()
            {
                message = "Ürün başarıyla güncellendi. ⚡",
                responseModel = new ProductResponseModel()
                {
                    id = product.id,
                    description = product.description,
                    name = product.name,
                    price = product.price,
                    photos = productPhotoModels,
                    productCategories = productCategories
                },
                statusCode = 200
            };

        }

        public ResponseViewModel filterProductsByName(string productName)
        {
            ICollection<ProductResponseModel> productResponseModels = new List<ProductResponseModel>();
            List<Product> products = productRepository.filterProductsByName(productName);
            if (products != null && products.Any())
            {
                foreach (Product item in products)
                {
                    List<ProductPhotoResponseModel> productPhotoModels = new List<ProductPhotoResponseModel>();
                    if (item.photos != null)
                    {
                        foreach (ProductPhoto photo in item.photos)
                        {
                            productPhotoModels.Add(new ProductPhotoResponseModel()
                            {
                                photoUrl = photo.photoUrl,
                                id = photo.id
                            });
                        }
                    }
                    ICollection<ProductCategoryResponseModel> productCategories = (HashSet<ProductCategoryResponseModel>)productProductCategoryService
                            .getProductCategoriesByProductId(item.id).responseModel;
                    ProductResponseModel responseModel = new ProductResponseModel()
                    {
                        description = item.description,
                        name = item.name,
                        id = item.id,
                        price = item.price,
                        productCategories = productCategories,
                        photos = productPhotoModels
                    };
                    productResponseModels.Add(responseModel);
                }
            }
            return new ResponseViewModel()
            {
                message = "Veri getirildi. 😍",
                responseModel = productResponseModels,
                statusCode = 200
            };
        }

        public ResponseViewModel filterProductsByNameAndCategory(string categoryId, string productName)
        {
            ICollection<ProductResponseModel> productResponseModels = new List<ProductResponseModel>();
            if (Convert.ToString(categoryId) == "+")
            {
                List<Product> products = productRepository.filterProductsByName(productName);
                foreach (Product item in products)
                {
                    List<ProductPhotoResponseModel> productPhotoModels = new List<ProductPhotoResponseModel>();
                    if (item.photos != null)
                    {
                        foreach (ProductPhoto photo in item.photos)
                        {
                            productPhotoModels.Add(new ProductPhotoResponseModel()
                            {
                                photoUrl = photo.photoUrl,
                                id = photo.id
                            });
                        }
                    }
                    ICollection<ProductCategoryResponseModel> productCategories = (HashSet<ProductCategoryResponseModel>)productProductCategoryService
                            .getProductCategoriesByProductId(item.id).responseModel;
                    ProductResponseModel responseModel = new ProductResponseModel()
                    {
                        description = item.description,
                        name = item.name,
                        id = item.id,
                        price = item.price,
                        productCategories = productCategories,
                        photos = productPhotoModels
                    };
                    productResponseModels.Add(responseModel);
                }
            }
            else
            {
                ICollection<ProductProductCategory> categories = productProductCategoryRespository.filterByCategoryId(new Guid(categoryId));
                if (categories != null && categories.Any())
                {
                    ICollection<Guid> prodcutsIds = categories.Select(p => p.productId).ToList();
                    List<Product> products = productRepository.filterProductsByNameAndCategory(prodcutsIds.ToList(), productName);
                    if (products != null && products.Any())
                    {
                        foreach (Product item in products)
                        {
                            List<ProductPhotoResponseModel> productPhotoModels = new List<ProductPhotoResponseModel>();
                            if (item.photos != null)
                            {
                                foreach (ProductPhoto photo in item.photos)
                                {
                                    productPhotoModels.Add(new ProductPhotoResponseModel()
                                    {
                                        photoUrl = photo.photoUrl,
                                        id = photo.id
                                    });
                                }
                            }
                            ICollection<ProductCategoryResponseModel> productCategories = (HashSet<ProductCategoryResponseModel>)productProductCategoryService
                                    .getProductCategoriesByProductId(item.id).responseModel;
                            ProductResponseModel responseModel = new ProductResponseModel()
                            {
                                description = item.description,
                                name = item.name,
                                id = item.id,
                                price = item.price,
                                productCategories = productCategories,
                                photos = productPhotoModels
                            };
                            productResponseModels.Add(responseModel);
                        }
                    }
                }
            }
            return new ResponseViewModel()
            {
                message = "Veri getirildi. 😍",
                responseModel = productResponseModels,
                statusCode = 200
            };
        }


    }
}
