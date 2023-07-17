using AmazonClone.Data.Context;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Data.Repositories
{
    public class CommentPhotoRepository : Repository<CommentPhoto>, ICommentPhotoRepository
    {
        public CommentPhotoRepository(BaseContext db) : base(db)
        {
        }
    }
}
