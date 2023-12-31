﻿using AmazonClone.Application.Interfaces;
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

        public ResponseViewModel ProductsByBoughtId(Guid id)
        {
            List<BoughtProductResponseModel> productResponseModels = boughtProductRespository.getProductsByBoughtId(id);
            return new ResponseViewModel()
            {
                message = "Başarıyla getirildi. 🌝",
                statusCode = 200,
                responseModel = productResponseModels,
            };
        }


        public ResponseViewModel deleteByBoughtId(Guid id)
        {
            bool v = boughtProductRespository.deleteProductsByBoughtId(id);
            if (v)
            {
                return new ResponseViewModel()
                {
                    message = "Başarıyla silindi. 🌝",
                    statusCode = 200,
                    responseModel = new object(),
                };
            }
            else
            {
                return new ResponseViewModel()
                {
                    message = "Başarıyla silinemedi. 😒",
                    statusCode = 400,
                    responseModel = new object(),
                };
            }
        }

    }
}
