namespace AmazonClone.Domain.Entities
{
    public class ProductPhotoCreateModel
    {
        public string photoUrl { get; set; }
        public Guid productId { get; set; }
    }
}
