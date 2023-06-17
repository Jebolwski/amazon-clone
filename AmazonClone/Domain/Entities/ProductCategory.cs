namespace AmazonClone.Domain.Entities
{
    public class ProductCategory : Entity
    {
        public string name { get; set; }
        public string description { get; set; }
        public ICollection<Product> products { get; set; }

    }
}
