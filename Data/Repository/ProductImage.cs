using Eshop.Data.Data;
using Eshop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Repository
{
    public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
    {
        private readonly ApplicationDbContext context;

        public ProductImageRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }


        public void update(ProductImage ProductImage)
        {
            context.Update(ProductImage);
        }
    }
}
