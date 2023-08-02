namespace AmazonClone.Domain.Entities
{
    public class CreditCart : Entity
    {
        public Guid userId { get; set; }
        public string nameSurname { get; set; }
        public string cartNumber { get; set; }
        public string cvvNumber { get; set; }
        public string expDate { get; set; }
    }
}