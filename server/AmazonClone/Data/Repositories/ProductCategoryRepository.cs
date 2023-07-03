using AmazonClone.Data.Context;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Data.Repositories
{
    public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(BaseContext db) : base(db)
        {
        }

        public ICollection<ProductCategory> GetProductCategories(){
            return dbset.ToList();
        }
    }
}
