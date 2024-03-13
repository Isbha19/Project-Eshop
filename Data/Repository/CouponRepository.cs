using Eshop.Data.Data;
using Eshop.Model.Models;


namespace Eshop.Data.Repository
{
    public class CouponRepository : Repository<Coupon>, ICouponRepository
    {
        private readonly ApplicationDbContext context;

        public CouponRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }


        public void update(Coupon coupon)
        {
            context.Update(coupon);
        }
    }
}
