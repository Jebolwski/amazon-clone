using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Application.ViewModels.ProductCategoryM;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Application.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository categoryRepository;

        public ProductCategoryService(IProductCategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public ProductCategoryResponseModel add(ProductCategoryCreateModel model)
        {
            if (model != null)
            {
                ProductCategory productCategory = new ProductCategory() { 
                    description = model.description,
                    name = model.name,
                };
                ProductCategory product = categoryRepository.add(productCategory);
                ProductCategoryResponseModel productCategoryResponse = new ProductCategoryResponseModel() {
                    description = product.description,
                    name = product.name
                };
                return productCategoryResponse;
            }
            return null;
        }

        public bool delete(Guid id)
        {
            ProductCategory productCategory = categoryRepository.get(id);
            if (productCategory != null)
            {
                categoryRepository.delete(id);
                return true;
            }
            return false;
        }

        public ProductCategoryResponseModel get(Guid id)
        {
            ProductCategory productCategory = categoryRepository.get(id);
            if (productCategory != null) {
                ProductCategoryResponseModel categoryResponseModel = new ProductCategoryResponseModel()
                {
                    description = productCategory.description,
                    name = productCategory.name,
                    id = productCategory.id
                };
                return categoryResponseModel;
            }
            return null;
        }

        public ProductCategoryResponseModel update(ProductUpdateModel model)
        {
            ProductCategory productCategory = categoryRepository.get(model.id);
            if (productCategory != null)
            {
                productCategory.description = model.description;
                productCategory.name = model.name;
                ProductCategory productCategory1 = categoryRepository.update(productCategory);
                ProductCategoryResponseModel responseModel = new ProductCategoryResponseModel()
                {
                    description= productCategory1.description,
                    name = productCategory1.name
                };
                return responseModel;
            }
            return null;
        }
    
        public ICollection<ProductCategoryResponseModel> GetProductCategories(){
            ICollection<ProductCategory> productCategories = categoryRepository.GetProductCategories();
            ICollection<ProductCategoryResponseModel> responseModel = new List<ProductCategoryResponseModel>();
            foreach(ProductCategory product in productCategories){
                responseModel.Add(new ProductCategoryResponseModel(){
                    description = product.description,
                    id = product.id,
                    name = product.name
                });
            }
            return responseModel;
        }
    }
}
