using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.CartProduct;
using AmazonClone.Application.ViewModels.CartProductM;
using AmazonClone.Domain.Interfaces;
using AmazonClone.Domain.Entities;
using AmazonClone.Application.ViewModels.ProductM;

namespace AmazonClone.Application.Services
{
    public class CartProductService : ICartProductService
    {
        private readonly ICartProductRepository cartProductRepository;
        private readonly IProductService productService;

        public CartProductService(ICartProductRepository cartProductRepository)
        {
            this.cartProductRepository = cartProductRepository;
        }

        public CartProductResponseModel add(CartProductCreateModel model)
        {
            CartProduct cartProduct = cartProductRepository.add(new CartProduct()
            {
                cartId = model.cartId,
                productId = model.productId
            });


            CartProductResponseModel cartProductResponseModel = new CartProductResponseModel()
            {
               id = cartProduct.id,
               products = new HashSet<ProductResponseModel>(),
            };

            return cartProductResponseModel;
        }

        public ICollection<ProductResponseModel> getProductsByCartId(Guid id)
        {
            ICollection<CartProduct> cartProducts = 
                cartProductRepository.getByCartId(id);
            ICollection<ProductResponseModel> productResponseModels = new HashSet<ProductResponseModel>();

            foreach (CartProduct item in cartProducts)
            {
                productResponseModels.Add(productService.get(item.productId));
            }

            return productResponseModels;
        }
    }
}
