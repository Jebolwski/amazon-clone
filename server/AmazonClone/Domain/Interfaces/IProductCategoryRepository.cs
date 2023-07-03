using AmazonClone.Domain.Entities;

namespace AmazonClone.Domain.Interfaces
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        public ICollection<ProductCategory> GetProductCategories();
    }
}
