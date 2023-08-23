using AmazonClone.Application.ViewModels.AuthM;
using AmazonClone.Application.ViewModels.BoughtProductM;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.ViewModels.BoughtM
{
    public class BoughtResponseModel
    {
        public DateTime? timeBought { get; set; }
        public UserResponseModel user { get; set; }
        public List<BoughtProductResponseModel> products { get; set; }
    }
}
