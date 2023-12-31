﻿namespace AmazonClone.Domain.Entities
{
    public class User : Entity
    {
        public Guid roleId { get; set; }
        public string username { get; set; }
        public byte[] passwordHash { get; set; }
        public byte[] passwordSalt { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public List<Address> addresses { get; set; }
        public Guid cartId { get; set; }
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
