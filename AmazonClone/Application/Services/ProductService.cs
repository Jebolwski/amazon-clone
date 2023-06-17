using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.Product;
using AmazonClone.Application.ViewModels.ProductCategory;
using AmazonClone.Data.Repositories;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public ProductResponseModel add(ProductCreateModel model)
        {
            
            if (model != null)
            {
                foreach (ProductCategoryCreateModel category in model.productCategories)
                {
                    //Burada veritabanına ürün kategorilerini ekle ama onun için ilk 
                    // önce ürün kategorisi servisini oluşturman gerekiyor
                    Console.WriteLine(category.name);
                }
                Product product = new Product()
                {
                    description = model.description,
                    name = model.name,
                    price = model.price,
                    carts = null,
                };
                product = productRepository.add(product);
                ProductResponseModel productResponse = new ProductResponseModel()
                {
                    description = product.description,
                    name = product.name,
                    price = product.price,
                    cart = null,
                    productCategories = new HashSet<ProductCategoryResponseModel>()
                };
                return productResponse;
            }
            return null;
        }

        public bool delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public ProductResponseModel get(Guid id)
        {
            throw new NotImplementedException();
        }

        public ProductResponseModel update(ProductUpdateModel model)
        {
            throw new NotImplementedException();
        }
    }
}
