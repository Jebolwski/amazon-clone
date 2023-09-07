using AmazonClone.Data.Context;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly IProductProductCategoryRepository productProductCategoryRepository;
        private readonly ICommentRepository commentRepository;
        public ProductRepository(BaseContext db, IProductProductCategoryRepository productProductCategoryRepository, ICommentRepository commentRepository) : base(db)
        {
            this.productProductCategoryRepository = productProductCategoryRepository;
            this.commentRepository = commentRepository;
        }

        public Product getProductWithPhotos(Guid id)
        {
            IQueryable<Product> product = dbset.AsNoTracking().Where(p => p.id == id).Include(x => x.photos).Include(x => x.comments);
            if (product != null && product.Any())
            {
                ICollection<Comment> comments = new List<Comment>();
                foreach (Comment comment in product.First().comments)
                {
                    comments.Add(commentRepository.getCommentWithPhotos(comment.id));
                }
                product.First().comments = comments;

                return new Product()
                {
                    comments = comments,
                    description = product.First().description,
                    id = product.First().id,
                    name = product.First().name,
                    price = product.First().price,
                    photos = product.First().photos
                };
            }
            return null;
        }

        public List<Product> filterProductsByName(string name)
        {
            if (name == "+")
            {
                return dbset.Include(x => x.photos).ToList();
            }
            List<Product> products = dbset.Where(p => p.name.ToLower().Contains(name.ToLower())).Include(x => x.photos).ToList(); ;
            if (products != null && products.Any())
            {
                return products;
            }
            return null;
        }

        public List<Product> filterProductsByNameAndCategory(List<Guid> productIds, string productName)
        {
            List<Product> products = new List<Product>();
            if (!productIds.Any())
            {
                if (productName == "+")
                {
                    products = dbset.ToList();
                }
                else
                {
                    products = dbset.Where(p => p.name.ToLower().Contains(productName.ToLower())).Include(x => x.photos).ToList();
                }
            }
            else
            {
                if (productName == "+")
                {
                    products = dbset.Where(p => productIds.Contains(p.id)).Include(x => x.photos).ToList();
                }
                else
                {
                    products = dbset.Where(p => p.name.ToLower().Contains(productName.ToLower()) && productIds.Contains(p.id)).Include(x => x.photos).ToList();
                }
            }
            if (products != null && products.Any())
            {
                return products;
            }
            return null;
        }
    }
}
