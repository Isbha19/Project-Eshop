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
    public class AddressRepository:Repository<ShippingAdress>,IAddressRepository
    {
        private readonly ApplicationDbContext context;

        public AddressRepository(ApplicationDbContext context):base(context)
        {
            this.context = context;
        }

     
        public void update(ShippingAdress AddressVal)
        {
            context.Update(AddressVal);
        }
    }
}
