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
    public class WishlistRepository:Repository<Wishlist>,IWishlistRepository
    {
        private readonly ApplicationDbContext context;

        public WishlistRepository(ApplicationDbContext context):base(context)
        {
            this.context = context;
        }

     
        public void update(Wishlist wishlist)
        {
            context.Update(wishlist);
        }
    }
}
