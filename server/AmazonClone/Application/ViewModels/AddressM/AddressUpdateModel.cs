namespace AmazonClone.Application.ViewModels.AddressM
{
    public class AddressUpdateModel
    {
        public Guid id { get; set; }
        public string city { get; set; }
        public string hood { get; set; }
        public string apartmentName { get; set; }
        public int apartmentNo { get; set; }
        public int floor { get; set; }
    }
}
