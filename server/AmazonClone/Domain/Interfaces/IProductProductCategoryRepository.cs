using AmazonClone.Domain.Entities;

namespace AmazonClone.Domain.Interfaces
{
    public interface IProductProductCategoryRepository : IRepository<ProductProductCategory>
    {
        ICollection<ProductProductCategory> FindByProductId(Guid id);
        bool DeleteItems(List<ProductProductCategory> items);
        bool deleteByProductCategoryId(Guid id);
        ICollection<ProductProductCategory> filterByCategoryId(Guid id);
    }
}
