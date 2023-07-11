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
            IQueryable<Product> product = dbset.AsNoTracking().Where(p => p.id == id).Include(x => x.photos);
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

        public List<Product> filterProductsByNameAndCategory(List<Guid> productIds, string productName)
        {
            List<Product> products = new List<Product>();

            if (productName == "+")
            {
                products = dbset.Where(p=>productIds.Contains(p.id)).Include(x => x.photos).ToList();
            }
            else
            {
                products = dbset.Where(p => p.name.ToLower().Contains(productName.ToLower()) && productIds.Contains(p.id)).Include(x => x.photos).ToList();
            }
            
            if (products != null && products.Any())
            {
                return products;
            }
            return null;
        }
    }
}
