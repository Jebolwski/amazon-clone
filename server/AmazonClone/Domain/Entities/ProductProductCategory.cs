namespace AmazonClone.Domain.Entities
{
    public class ProductProductCategory : Entity
    {
        public Guid productId { get; set; }
        public Guid productCategoryId { get; set; }
    }
}
