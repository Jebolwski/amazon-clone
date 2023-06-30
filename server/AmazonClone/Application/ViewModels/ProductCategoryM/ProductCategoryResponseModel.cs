using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.ViewModels.ProductCategoryM
{
    public class ProductCategoryResponseModel
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public static ProductCategory convert(ProductCategoryResponseModel model)
        {
            return new ProductCategory()
            {
                id = model.id,
                name = model.name,
                description = model.description,
            };
        }
    }
}
