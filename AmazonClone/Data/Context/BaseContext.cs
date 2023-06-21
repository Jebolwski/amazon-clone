using AmazonClone.Data.Mapping;
using AmazonClone.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.Data.Context
{
    public class BaseContext : IdentityDbContext
    {
        private readonly string dbUser = "admin";
        private readonly string dbPassword = "password";
        private readonly string dbHost = "localhost";
        private readonly string database = "admin";
        private readonly int dbPort = 5432;
        private readonly string connectionString;

        public DbSet<Cart> carts { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<CommentPhoto> commentPhotos { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<ProductCategory> productCategories { get; set; }
        public DbSet<ProductPhoto> productPhotos { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<User> users { get; set; }

        public BaseContext()
        {
            connectionString = $"User ID={dbUser};Password={dbPassword};Host={dbHost};Port={dbPort};Database={database};Pooling=true;";
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CartMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new ProductCategoryMap());
            modelBuilder.ApplyConfiguration(new ProductProductCategoryMap());
            modelBuilder.ApplyConfiguration(new CartProductMap());
            modelBuilder.ApplyConfiguration(new ProductPhotoMap());
            modelBuilder.ApplyConfiguration(new CommentMap());
            modelBuilder.ApplyConfiguration(new CommentPhotoMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.HasDefaultSchema("AmazonClone");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
