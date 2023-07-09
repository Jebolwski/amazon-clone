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

        public bool deleteByProductCategoryId(Guid id)
        {
            List<ProductProductCategory> productProductCategories = dbset.Where(p => p.productCategoryId == id).ToList();
            if (productProductCategories != null)
            {
                DeleteItems(productProductCategories);
                return true;
            }
            return false;
        }

        public bool DeleteItems(List<ProductProductCategory> items)
        {
            dbset.RemoveRange(items);
            db.SaveChanges();
            return true;
        }

        public ICollection<ProductProductCategory> FindByProductId(Guid id)
        {
            return dbset.Where(p => p.productId == id).ToList();
        }

    }
}
