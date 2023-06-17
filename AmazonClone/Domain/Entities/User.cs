namespace AmazonClone.Domain.Entities
{
    public class User : Entity
    {
        public Guid roleId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
