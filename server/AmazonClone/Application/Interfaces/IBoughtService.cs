using AmazonClone.Application.ViewModels.ResponseM;

namespace AmazonClone.Application.Interfaces
{
    public interface IBoughtService
    {
        public ResponseViewModel addBought(string authToken);
    }
}
