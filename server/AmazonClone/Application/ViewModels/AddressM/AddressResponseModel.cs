using AmazonClone.Domain.Entities;

namespace AmazonClone.Application.ViewModels.AddressM
{
    public class AddressResponseModel
    {
        public User user{ get; set; }
        public string city { get; set; }
        public string hood { get; set; }
        public string apartmentName { get; set; }
        public int apartmentNo { get; set; }
        public int floor { get; set; }
        public string addressComplete { get; set; }
    }
}
