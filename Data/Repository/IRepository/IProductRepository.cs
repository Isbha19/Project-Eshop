using Eshop.Model.Models;
using Eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        void update(Product product);
        void updateStock(Product product);
        void updateViewsCountandWishlist(Product product);
        void updateOrderCounts(Product product);
        void updateOffer(Product product);

    }
}
