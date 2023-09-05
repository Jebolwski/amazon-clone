namespace AmazonClone.Domain.Entities
{
    public class Refund : Entity
    {
        public Guid BoughtId { get; set; }
        public string RefundCode { get; set; }
    }
}
