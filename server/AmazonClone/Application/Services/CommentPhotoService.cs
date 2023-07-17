using AmazonClone.Application.Interfaces;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Application.Services
{
    public class CommentPhotoService : ICommentPhotoService
    {
        private readonly ICommentPhotoRepository commentPhotoRepository;

        public CommentPhotoService(ICommentPhotoRepository commentPhotoRepository)
        {
            this.commentPhotoRepository = commentPhotoRepository;
        }

        public bool delete(Guid id)
        {
            return this.commentPhotoRepository.delete(id);
        }
    }
}
