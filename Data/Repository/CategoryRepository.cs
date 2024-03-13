using Eshop.Data.Data;
using Eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Repository
{
    public class CategoryRepository:Repository<Category>,ICategoryRepository
    {
        private readonly ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext context):base(context)
        {
            this.context = context;
        }

     
        public void update(Category category)
        {
            context.Update(category);
        }
    }
}
