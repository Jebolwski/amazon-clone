using AmazonClone.Domain.Entities;

namespace AmazonClone.Domain.Interfaces
{
    public interface IProductProductCategoryRepository : IRepository<ProductProductCategory>
    {
        public ICollection<ProductProductCategory> FindByProductId(Guid id);
        public bool DeleteItems(List<ProductProductCategory> items);
        public bool deleteByProductCategoryId(Guid id);
    }
}
