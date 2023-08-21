using AmazonClone.Application.Services;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.ViewModels.BoughtM
{
    public class ResposneModel
    {
        public ICollection<ProductResponseModel> products { get; set; }
        public Cart cart { get; set; }
    }
}
