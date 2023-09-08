using AmazonClone.Data.Context;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.Data.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(BaseContext db) : base(db)
        {
        }

        public Comment getCommentWithPhotos(Guid id)
        {
            IQueryable<Comment> comment = dbset.Where(p => p.id == id).Include(x => x.commentPhotos);
            if (comment != null && comment.Any())
            {
                return comment.First();
            }
            return null;
        }

        public IQueryable<Comment> getUsersComments(Guid id)
        {
            IQueryable<Comment> comments = dbset.Where(p => p.userId == id).Include(x => x.commentPhotos);
            if (comments != null && comments.Any())
            {
                return comments;
            }
            return null;
        }
    }
}
