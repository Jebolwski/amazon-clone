using AmazonClone.Application.ViewModels.ProductCategoryM;
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

        public ICollection<ProductProductCategory> FindByProductId(Guid id)
        {
            return dbset.Where(p=>p.productId == id).ToList();
        }
    }
}
