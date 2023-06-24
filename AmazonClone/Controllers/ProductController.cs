using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IProductRepository productRepository;

        public ProductController(IProductService productService, IProductRepository productRepository)
        {
            this.productService = productService;
            this.productRepository = productRepository;
        }

        [HttpPost("add"),Authorize(Roles = "Admin")]
        public ProductResponseModel add(ProductCreateModel model)
        {
            return productService.add(model);
        }


        [HttpGet("{productId}"), AllowAnonymous]
        public ProductResponseModel get(Guid productId)
        {
            return productService.get(productId);
        }


        [HttpDelete("{productId}"), Authorize(Roles = "Admin")]
        public bool delete(Guid productId)
        {
            return productService.delete(productId);
        }

        [HttpPut("update"), Authorize(Roles = "Admin")]
        public ProductResponseModel update(ProductUpdateModel model)
        {
            return productService.update(model);
        }


    }
}
