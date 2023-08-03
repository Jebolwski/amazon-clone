using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.ViewModels.CreditCartM
{
    public class CreditCartResponseModel
    {
        public Guid id{ get; set; }
        public User user { get; set; }
        public string nameSurname { get; set; }
        public string cartNumber { get; set; }
        public string cvvNumber { get; set; }
        public string expDate { get; set; }
    }
}
