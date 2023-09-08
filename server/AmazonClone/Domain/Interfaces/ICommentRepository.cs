using AmazonClone.Domain.Entities;

namespace AmazonClone.Domain.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        public Comment getCommentWithPhotos(Guid id);
        public IQueryable<Comment> getUsersComments(Guid id);
    }
}
