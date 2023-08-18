namespace AmazonClone.Domain.Entities
{
    public class BoughtProduct : Entity
    {
        public Guid boughtId { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public string description { get; set; }
        public Guid productId { get; set; }
    }
}
