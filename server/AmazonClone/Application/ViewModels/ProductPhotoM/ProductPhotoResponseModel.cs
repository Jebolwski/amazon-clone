using AmazonClone.Application.ViewModels.ProductCategoryM;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Application.ViewModels.ProductPhotoM
{
    public class ProductPhotoResponseModel
    {
        public Guid id { get; set; }
        public string photoUrl { get; set; }

        }
}
