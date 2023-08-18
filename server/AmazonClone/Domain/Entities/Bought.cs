namespace AmazonClone.Domain.Entities
{
    public class Bought : Entity
    {
        public DateTime? timeBought { get; set; }
        public List<BoughtProduct> products { get; set; }
        public Guid userId { get; set; }
    }
}
