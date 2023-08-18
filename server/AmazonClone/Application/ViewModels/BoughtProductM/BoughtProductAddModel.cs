namespace AmazonClone.Application.ViewModels.BoughtProductM
{
    public class BoughtProductAddModel
    {
        public Guid boughtId { get; set; }
        public string name { get; set; }
        public float price { get; set; }
        public string description { get; set; }
        public Guid productId { get; set; }
    }
}
