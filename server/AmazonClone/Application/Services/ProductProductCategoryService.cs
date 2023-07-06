using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.ProductCategoryM;
using AmazonClone.Application.ViewModels.ProductProductCategory;
using AmazonClone.Application.ViewModels.ResponseM;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;
using System.Text.Json;

namespace AmazonClone.Application.Services
{
    public class ProductProductCategoryService : IProductProductCategoryService
    {
        private readonly IProductProductCategoryRepository productProductCategoryRepository;
        private readonly IProductCategoryService productCategoryService;

        public ProductProductCategoryService(IProductProductCategoryRepository productProductCategoryRepository,
            IProductCategoryService productCategoryService)
        {
            this.productProductCategoryRepository = productProductCategoryRepository;
            this.productCategoryService = productCategoryService;
        }

        public ResponseViewModel add(ProductProductCategoryCreateModel model)
        {
            ProductCategoryResponseModel productCategoryResponseModel = (ProductCategoryResponseModel)productCategoryService.get(model.productCategoryId).responseModel;
            if (productCategoryResponseModel == null)
            {
                return new ResponseViewModel()
                {
                    message = "Veri bulunamadı. 😢",
                    responseModel = new Object(),
                    statusCode = 400
                };
            }
            ProductProductCategory productProductCtg = productProductCategoryRepository.add(new ProductProductCategory()
            {
                productCategoryId = model.productCategoryId,
                productId = model.productId,

            });
            return new ResponseViewModel()
            {
                message = "Eklendi. 🥰",
                responseModel = productCategoryResponseModel,
                statusCode = 200
            };
        }

        public bool delete(Guid id)
        {
            return productProductCategoryRepository.delete(id);
        }
        public ResponseViewModel get(Guid id)
        {
            ProductProductCategory productProductCategory = productProductCategoryRepository.get(id);
            if (productProductCategory != null)
            {
                return new ResponseViewModel()
                {
                    message = "Veri getirildi. 🥰",
                    responseModel = productProductCategory,
                    statusCode = 200
                };
            }
            return new ResponseViewModel()
            {
                message = "Bulunamadı. 😒",
                responseModel = new Object(),
                statusCode = 400
            };

        }

        public ResponseViewModel getProductCategoriesByProductId(Guid productId)
        {
            ICollection<ProductProductCategory> productProductCategories = productProductCategoryRepository.FindByProductId(productId);
            ICollection<ProductCategoryResponseModel> productCategoryResponseModels = new HashSet<ProductCategoryResponseModel>();

            foreach (ProductProductCategory item in productProductCategories)
            {
                //productCategoryResponseModels.Add((ProductCategoryResponseModel)productCategoryService
                //    .get(item.productCategoryId).responseModel);
                string json = JsonSerializer
                    .Serialize(productCategoryService.get(item.productCategoryId).responseModel);
                if (json.Equals("{}")==false) {
                    ProductCategoryResponseModel productCategoryResponseModel = JsonSerializer.Deserialize<ProductCategoryResponseModel>(json);
                    productCategoryResponseModels.Add(productCategoryResponseModel);
                }
            }

            return new ResponseViewModel()
            {
                message = "Veriler getirildi. 🥰",
                responseModel = productCategoryResponseModels,
                statusCode = 200
            };
        }

        public ResponseViewModel deleteProductProductCategoriesByProductId(Guid productId)
        {
            List<ProductProductCategory> productProductCategories = productProductCategoryRepository
                .FindByProductId(productId).ToList();
            if (productProductCategories == null)
            {
                return new ResponseViewModel()
                {
                    message = "Veri bulunamadı. 😐",
                    responseModel = new Object(),
                    statusCode = 400
                };
            }
            productProductCategoryRepository.DeleteItems(productProductCategories);
            return new ResponseViewModel()
            {
                message = "Başarıyla silindi. 🥰",
                responseModel = new Object(),
                statusCode = 200
            };
        }

        public bool deleteProductProductCategoriesByProductCategoryId(Guid productCategoryId)
        {
            return productProductCategoryRepository.deleteByProductCategoryId(productCategoryId);
        }
    }
}
