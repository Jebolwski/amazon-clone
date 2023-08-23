using AmazonClone.Application.ViewModels.BoughtProductM;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Domain.Entities;

namespace AmazonClone.Domain.Interfaces
{
    public interface IBoughtProductRespository : IRepository<BoughtProduct>
    {
        public List<BoughtProductResponseModel> getProductsByBoughtId(Guid id);
        public bool deleteProductsByBoughtId(Guid id);
    }
}
