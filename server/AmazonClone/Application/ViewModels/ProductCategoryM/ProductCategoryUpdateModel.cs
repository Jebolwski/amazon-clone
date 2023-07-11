using AmazonClone.Application.ViewModels.ProductPhotoM;
using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.ViewModels.ProductCategoryM
{
    public class ProductCategoryUpdateModel
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
