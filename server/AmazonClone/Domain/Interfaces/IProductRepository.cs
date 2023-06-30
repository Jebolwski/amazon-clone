using AmazonClone.Domain.Entities;

namespace AmazonClone.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Product getProductWithPhotos(Guid id); 
    }
}
