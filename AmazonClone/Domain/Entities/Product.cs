using System.Reflection.Metadata;

namespace AmazonClone.Domain.Entities
{
    public class Product : Entity
    {
        public string name { get; set; }
        public float price { get; set; }
        public string description { get; set; }
        public ICollection<ProductPhoto> photos { get; set; }
        public ICollection<Comment> comments { get; set; }
    }
}
