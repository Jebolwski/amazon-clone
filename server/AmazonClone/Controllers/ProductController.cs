using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Application.ViewModels.ResponseM;
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

        [HttpPost("add"), Authorize(Roles = "Admin")]
        public ResponseViewModel add(ProductCreateModel model)
        {
            return productService.add(model);
        }


        [HttpGet("{productId}"), AllowAnonymous]
        public ResponseViewModel get(Guid productId)
        {
            return productService.get(productId);
        }


        [HttpDelete("{productId}"), Authorize(Roles = "Admin")]
        public ResponseViewModel delete(Guid productId)
        {
            return productService.delete(productId);
        }

        [HttpPut("update"), Authorize(Roles = "Admin")]
        public ResponseViewModel update(ProductUpdateModel model)
        {
            return productService.update(model);
        }

        [HttpGet("filter-by-name/{productName}"), AllowAnonymous]
        public ResponseViewModel filterProductsByName(string productName)
        {
            return productService.filterProductsByName(productName);
        }

        [HttpGet("filter-by-name-and-category/{productName}/{categoryId}"), AllowAnonymous]
        public ResponseViewModel filterProductsByNameAndCategory(string categoryId, string productName)
        {
            return productService.filterProductsByNameAndCategory(categoryId, productName);
        }
    }
}
