using Eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop.Model.Models;
namespace Eshop.Data.Repository
{
    public interface IProductImageRepository : IRepository<ProductImage>
    {
        void update(ProductImage productImage);


    }
}
