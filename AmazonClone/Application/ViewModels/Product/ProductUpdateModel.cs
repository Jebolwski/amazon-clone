using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.ViewModels.Product
{
    public class ProductUpdateModel
    {
        public string name { get; set; }
        public float price { get; set; }
        public string description { get; set; }
    }
}
