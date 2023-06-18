using AmazonClone.Data.Context;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Data.Repositories
{
    public class ProductPhotoRepository : Repository<ProductPhoto>, IProductPhotoRepository
    {
        public ProductPhotoRepository(BaseContext db) : base(db)
        {
        }
    }
}
