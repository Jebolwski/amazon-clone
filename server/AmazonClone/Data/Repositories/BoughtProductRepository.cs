﻿using AmazonClone.Application.Interfaces;
using AmazonClone.Application.Services;
using AmazonClone.Application.ViewModels.BoughtProductM;
using AmazonClone.Application.ViewModels.CommentM;
using AmazonClone.Application.ViewModels.CommentPhotoM;
using AmazonClone.Application.ViewModels.ProductCategoryM;
using AmazonClone.Application.ViewModels.ProductM;
using AmazonClone.Application.ViewModels.ProductPhotoM;
using AmazonClone.Data.Context;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;

namespace AmazonClone.Data.Repositories
{
    public class BoughtProductRepository : Repository<BoughtProduct>, IBoughtProductRespository
    {
        private readonly ICommentRepository commentRepository;
        private readonly IProductRepository productRepository;
        private readonly IProductProductCategoryService productProductCategoryService;
        public BoughtProductRepository(BaseContext db, ICommentRepository commentRepository, IProductRepository productRepository, IProductProductCategoryService productProductCategoryService) : base(db)
        {
            this.commentRepository = commentRepository;
            this.productRepository = productRepository;
            this.productProductCategoryService = productProductCategoryService;
        }

        public bool deleteProductsByBoughtId(Guid id)
        {
            List<BoughtProduct> boughtProducts = dbset.Where(p => p.boughtId == id).ToList();
            if (boughtProducts != null)
            {
                DeleteItems(boughtProducts);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteItems(List<BoughtProduct> items)
        {
            dbset.RemoveRange(items);
            db.SaveChanges();
            return true;
        }

        public List<BoughtProductResponseModel> getProductsByBoughtId(Guid id)
        {
            List<BoughtProduct> boughtProducts = dbset.Where(p => p.boughtId == id).ToList();
            List<BoughtProductResponseModel> liste = new List<BoughtProductResponseModel>();

            foreach (BoughtProduct product in boughtProducts)
            {

                Product product1 = productRepository.getProductWithPhotos(product.productId);
                HashSet<ProductPhotoResponseModel> productPhotoModels = new HashSet<ProductPhotoResponseModel>();
                foreach (ProductPhoto photo in product1.photos)
                {
                    productPhotoModels.Add(new ProductPhotoResponseModel()
                    {
                        photoUrl = photo.photoUrl,
                        id = photo.id
                    });
                }
                List<CommentResponseModel> commentResponseModel = new List<CommentResponseModel>();
                foreach (Comment comment in product1.comments)
                {
                    Comment comment1 = commentRepository.getCommentWithPhotos(comment.id);
                    List<CommentPhotoResponseModel> commentPhotos = new List<CommentPhotoResponseModel>();
                    foreach (CommentPhoto commentPhoto in comment1.commentPhotos)
                    {
                        commentPhotos.Add(new CommentPhotoResponseModel()
                        {
                            id = commentPhoto.id,
                            photoUrl = commentPhoto.photoUrl
                        });
                    }

                    commentResponseModel.Add(new CommentResponseModel()
                    {
                        comment = comment1.comment,
                        id = comment1.id,
                        productId = comment1.productId,
                        stars = comment1.stars,
                        title = comment1.title,
                        user = null,
                        commentPhotos = commentPhotos,
                    });
                }
                ICollection<ProductCategoryResponseModel> productCategories = (HashSet<ProductCategoryResponseModel>)productProductCategoryService
                    .getProductCategoriesByProductId(id).responseModel;
                liste.Add(new BoughtProductResponseModel()
                {
                    boughtId = product.boughtId,
                    productId = product.productId,
                    description = product.description,
                    photos = productPhotoModels,
                    comments = commentResponseModel,
                    id = product.id,
                    name = product.name,
                    price = product.price,
                    productCategories = productCategories,
                });
            }
            return liste;
        }
    }
}
