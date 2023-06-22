using AmazonClone.Application.ViewModels.ProductM;

namespace AmazonClone.Application.ViewModels.CartM
{
    public interface CartResponseModel
    {
        public Guid userId { get; set; }
        public ICollection<ProductResponseModel> products { get; set; } 
    }
}
