using Eshop.Model.Models;
using Eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Repository
{
    public interface ICartRepository:IRepository<ShoppingCart>
    {
        void update(ShoppingCart cart);
   

    }
}
