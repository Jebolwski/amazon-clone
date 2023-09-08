using AmazonClone.Application.ViewModels.ResponseM;

namespace AmazonClone.Application.Interfaces
{
    public interface IBoughtService
    {
        public ResponseViewModel addBought(string authToken);
        public ResponseViewModel getBoughts(string authToken);
        public ResponseViewModel deleteBoughts(string authToken, Guid id);
        public ResponseViewModel getArchivedBoughts(string authToken);
        public ResponseViewModel toggleBoughts(string authToken, Guid id);
    }
}
