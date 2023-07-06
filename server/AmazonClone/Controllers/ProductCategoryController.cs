using AmazonClone.Application.Interfaces;
using AmazonClone.Application.ViewModels.ProductCategoryM;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Application.ViewModels.ResponseM;
using AmazonClone.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazonClone.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            this.productCategoryService = productCategoryService;
        }

        [HttpGet("get-all-categories"),Authorize(Roles = "Admin")]
        public ResponseViewModel GetAllCategories(){
            return this.productCategoryService.GetProductCategories();
        }

        [HttpPost("add"),Authorize(Roles = "Admin")]
        public ResponseViewModel Add(ProductCategoryCreateModel model){
            return this.productCategoryService.add(model);
        }

        [HttpDelete("{categoryId}/delete"),Authorize(Roles = "Admin")]
        public ResponseViewModel Delete(Guid categoryId){
            return this.productCategoryService.delete(categoryId);
        }

        [HttpGet("{categoryId}"),Authorize(Roles = "Admin")]
        public ResponseViewModel GetAll(Guid categoryId){
            return this.productCategoryService.get(categoryId);
        }
    }
}
