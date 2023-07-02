using AmazonClone.Data.Context;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(BaseContext db) : base(db)
        {
        }

        public Product getProductWithPhotos(Guid id)
        {
            IQueryable<Product> product = dbset.Where(p => p.id == id).Include(x=>x.photos);
            if (product != null && product.Any()) {
                return product.First();
            }
            return null;
        }

        public List<Product> filterProductsByName(string name)
        {
            List<Product> products = dbset.Where(p => p.name.Contains(name)).Include(x => x.photos).ToList(); ;
            if (products != null && products.Any())
            {
                return products;
            }
            return null;
        }
    }
}
