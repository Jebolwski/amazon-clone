using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Application.ViewModels.ProductCategoryM;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;
using AmazonClone.Application.ViewModels.ResponseM;

namespace AmazonClone.Application.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository categoryRepository;

        public ProductCategoryService(IProductCategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public ResponseViewModel add(ProductCategoryCreateModel model)
        {
            if (model != null)
            {
                ProductCategory productCategory = new ProductCategory()
                {
                    description = model.description,
                    name = model.name,
                };
                ProductCategory product = categoryRepository.add(productCategory);
                ProductCategoryResponseModel productCategoryResponse = new ProductCategoryResponseModel()
                {
                    description = product.description,
                    name = product.name,
                    id = product.id
                };
                return new ResponseViewModel()
                {
                    responseModel = productCategoryResponse,
                    message = "Ürün kategorisi başarıyla eklendi. ✨",
                    statusCode = 200
                };
            }
            return new ResponseViewModel()
            {
                message = "Veri eklenmedi. 😥",
                responseModel = new Object(),
                statusCode = 400
            };
        }

        public ResponseViewModel delete(Guid id)
        {
            ProductCategory productCategory = categoryRepository.get(id);
            if (productCategory != null)
            {
                categoryRepository.delete(id);
                return new ResponseViewModel()
                {
                    message = "Ürün kategorisi başarıyla silindi. 😍",
                    responseModel = new Object(),
                    statusCode = 200
                };
            }
            return new ResponseViewModel()
            {
                message = "Verdiğiniz id ile ürün bulunamadı. 😐",
                responseModel = new Object(),
                statusCode = 400
            };
        }

        public ResponseViewModel get(Guid id)
        {
            ProductCategory productCategory = categoryRepository.get(id);
            if (productCategory != null)
            {
                ProductCategoryResponseModel categoryResponseModel = new ProductCategoryResponseModel()
                {
                    description = productCategory.description,
                    name = productCategory.name,
                    id = productCategory.id
                };
                return new ResponseViewModel()
                {
                    message = "Ürün kategorisi getirildi. 🚀",
                    responseModel = categoryResponseModel,
                    statusCode = 200
                };
            }
            return new ResponseViewModel()
            {
                message = "Ürün kategorisi bulunamadı. 😒",
                responseModel = new Object(),
                statusCode = 400
            };
        }

        public ResponseViewModel update(ProductCategoryUpdateModel model)
        {
            ProductCategory productCategory = categoryRepository.get(model.id);
            if (productCategory != null)
            {
                productCategory.description = model.description;
                productCategory.name = model.name;
                ProductCategory productCategory1 = categoryRepository.update(productCategory);
                ProductCategoryResponseModel responseModel = new ProductCategoryResponseModel()
                {
                    id = productCategory1.id,
                    description = productCategory1.description,
                    name = productCategory1.name
                };
                return new ResponseViewModel()
                {
                    message = "Ürün kategorisi başarıyla güncellendi. ⚡",
                    responseModel = responseModel,
                    statusCode = 200
                };
            }
            return new ResponseViewModel()
            {
                message = "Ürün kategorisi bulunamadı. 😶",
                responseModel = new Object(),
                statusCode = 400
            };
        }

        public ResponseViewModel GetProductCategories()
        {
            ICollection<ProductCategory> productCategories = categoryRepository.GetProductCategories();
            ICollection<ProductCategoryResponseModel> responseModel = new List<ProductCategoryResponseModel>();
            foreach (ProductCategory product in productCategories)
            {
                responseModel.Add(new ProductCategoryResponseModel()
                {
                    description = product.description,
                    id = product.id,
                    name = product.name
                });
            }
            return new ResponseViewModel()
            {
                message = "Ürün kategorileri getirildi. ⚡",
                responseModel = responseModel,
                statusCode = 200
            };
        }
    }
}
