using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Domain.Entities;

namespace AmazonClone.Domain.Interfaces
{
    public interface IBoughtProductRespository : IRepository<BoughtProduct>
    {
        public List<ProductResponseModel> getProductsByBoughtId(Guid id);
    }
}
