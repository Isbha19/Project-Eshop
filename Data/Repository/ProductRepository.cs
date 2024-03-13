using Eshop.Data.Data;
using Eshop.Model.Models;
using Eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }


        public void update(Product product)
        {
            var productFromDb=context.Products.FirstOrDefault(u=>u.Id==product.Id);
            if (productFromDb!=null)
            {
                productFromDb.ProductName = product.ProductName;
                productFromDb.ProductDescription = product.ProductDescription;  
                productFromDb.ProductPrice= product.ProductPrice;
                productFromDb.productImages = product.productImages;
                productFromDb.ColorsId = product.ColorsId;
                productFromDb.CategoryId= product.CategoryId;
                productFromDb.Quantity = product.Quantity;
                productFromDb.isFeatured = product.isFeatured;
                if (productFromDb.StockLeft <= productFromDb.Quantity)
                {
                    productFromDb.StockLeft = product.StockLeft;
                }
            }
        }
        public void updateStock(Product product)
        {
            var productFromDb = context.Products.FirstOrDefault(u => u.Id == product.Id);
            if (productFromDb!=null)
            {
                     productFromDb.StockLeft = product.StockLeft;

            }

        }

       
        void IProductRepository.updateViewsCountandWishlist(Product product)
        {
            var productFromDb = context.Products.FirstOrDefault(u => u.Id == product.Id);

            if (productFromDb != null)
            {
                productFromDb.ViewsCount = product.ViewsCount;
                productFromDb.isWishlisted = product.isWishlisted;

            }
        }
        public void  updateOrderCounts(Product product)
        {
            var productFromDb = context.Products.FirstOrDefault(u => u.Id == product.Id);

            if (productFromDb != null)
            {
                productFromDb.OrderCount = product.OrderCount;

            }
        }

        public void updateOffer(Product product)
        {
            var productFromDb = context.Products.FirstOrDefault(u => u.Id == product.Id);

            if (productFromDb != null)
            {
                productFromDb.isOffered = product.isOffered;
                productFromDb.OfferPrice= product.OfferPrice;
                productFromDb.OfferName= product.OfferName;
                productFromDb.OfferType= product.OfferType;

            }
        }
    }
}
