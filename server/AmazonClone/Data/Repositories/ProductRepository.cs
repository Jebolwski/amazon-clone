using AmazonClone.Data.Context;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly IProductProductCategoryRepository productProductCategoryRepository;

        public ProductRepository(BaseContext db, IProductProductCategoryRepository productProductCategoryRepository) : base(db)
        {
            this.productProductCategoryRepository = productProductCategoryRepository;
        }

        public Product getProductWithPhotos(Guid id)
        {
            IQueryable<Product> product = dbset.Where(p => p.id == id).Include(x => x.photos);
            if (product != null && product.Any())
            {
                return product.First();
            }
            return null;
        }

        public List<Product> filterProductsByName(string name)
        {
            List<Product> products = dbset.Where(p => p.name.ToLower().Contains(name.ToLower())).Include(x => x.photos).ToList(); ;
            if (products != null && products.Any())
            {
                return products;
            }
            return null;
        }

        public List<Product> filterProductsByNameAndCategory(Guid categoryId, string productName)
        {
            List<Product> products = new List<Product>();
            if (productName == "+")
            {
                products = dbset.Include(x => x.photos).ToList();
            }
            else
            {
                products = dbset.Where(p => p.name.ToLower().Contains(productName.ToLower())).Include(x => x.photos).ToList();
            }
            List<Product> products1 = new List<Product>();
            foreach (Product product in products)
            {
                ICollection<ProductProductCategory> productProductCategories = productProductCategoryRepository.FindByProductId(product.id);
                foreach (ProductProductCategory category in productProductCategories)
                {
                    if (category.productCategoryId == categoryId)
                    {
                        products1.Add(product);
                    }
                }
            }
            if (products1 != null && products1.Any())
            {
                return products1;
            }
            return null;
        }
    }
}
