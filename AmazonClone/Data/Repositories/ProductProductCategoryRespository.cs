using AmazonClone.Data.Context;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Data.Repositories
{
    public class ProductProductCategoryRepository : Repository<ProductProductCategory>, IProductProductCategoryRepository
    {
        public ProductProductCategoryRepository(BaseContext db) : base(db)
        {
        }
    }
}
