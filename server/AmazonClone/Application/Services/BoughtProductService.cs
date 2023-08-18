using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.BoughtProductM;
using AmazonClone.Application.ViewModels.ResponseM;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Application.Services
{
    public class BoughtProductService : IBoughtProductService
    {
        private readonly IBoughtProductRespository boughtProductRespository;

        public BoughtProductService(IBoughtProductRespository boughtProductRespository)
        {
            this.boughtProductRespository = boughtProductRespository;
        }

        public ResponseViewModel addBoughtProduct(BoughtProductAddModel model)
        {
            BoughtProduct boughtProduct = new BoughtProduct()
            {
                boughtId = model.boughtId,
                description = model.description,
                name = model.name,
                price = model.price,
                productId = model.productId,
            };

            BoughtProduct boughtProductCreated = boughtProductRespository.add(boughtProduct);

            return new ResponseViewModel()
            {
                message = "Başarıyla eklendi. 🚀",
                responseModel = boughtProductCreated,
                statusCode = 200
            };
        }
    }
}
