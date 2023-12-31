﻿using AmazonClone.Application.ViewModels.BoughtProductM;
using AmazonClone.Application.ViewModels.ResponseM;

namespace AmazonClone.Application.Interfaces
{
    public interface IBoughtProductService
    {
        public ResponseViewModel addBoughtProduct(BoughtProductAddModel model);
        public ResponseViewModel ProductsByBoughtId(Guid id);
        public ResponseViewModel deleteByBoughtId(Guid id);
    }
}
