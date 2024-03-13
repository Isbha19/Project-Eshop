using Eshop.Model.Models;
using Eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Repository
{
    public interface IWishlistRepository:IRepository<Wishlist>
    {
        void update(Wishlist wishlist);
   

    }
}
