namespace AmazonClone.Domain.Entities
{
    public class Address : Entity
    {
        public Guid userId { get; set; }
        public string city { get; set; }
        public string hood { get; set; }
        public string apartmentName { get; set; }
        public int apartmentNo { get; set; }
        public int floor { get; set; }
        public string addressComplete { get; set; }

    }
}
