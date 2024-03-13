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
    public class UserRepository:Repository<ApplicationUser>,IUserRepository
    {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context):base(context)
        {
            this.context = context;
        }

     
        public void update(ApplicationUser appUser)
        {
            context.Update(appUser);
        }
    }
}
