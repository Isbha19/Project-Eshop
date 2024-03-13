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
    public class CartRepository:Repository<ShoppingCart>,ICartRepository
    {
        private readonly ApplicationDbContext context;

        public CartRepository(ApplicationDbContext context):base(context)
        {
            this.context = context;
        }

     
        public void update(ShoppingCart cart)
        {
            context.Update(cart);
        }
    }
}
