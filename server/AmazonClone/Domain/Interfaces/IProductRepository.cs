﻿using AmazonClone.Domain.Entities;

namespace AmazonClone.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Product getProductWithPhotos(Guid id);
        List<Product> filterProductsByName(string name);
        List<Product> filterProductsByNameAndCategory(Guid categoryId, string productName);
    }
}
