﻿using AmazonClone.Data.Context;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Data.Repositories
{
    public class CartProductRepository : Repository<CartProduct>, ICartProductRepository
    {
        public CartProductRepository(BaseContext db) : base(db)
        {
        }

        public ICollection<CartProduct> getByCartId(Guid id)
        {
            return dbset.Where(p => p.cartId == id).ToList();
        }

        public bool deleteByCartIdAndProductId(Guid productId, Guid cartId)
        {
            List<CartProduct> cartProducts = dbset.Where(p => p.cartId == cartId).Where(p => p.productId == productId).ToList();
            System.Console.WriteLine(cartProducts.Any());
            if (cartProducts != null && cartProducts.Any())
            {
                CartProduct cartProduct = cartProducts.First();
                delete(cartProduct.id);
                return true;
            }
            return false;
        }
    }
}
