using Eshop.Model.Models;




namespace Eshop.Data.Repository
{
    public interface ICouponRepository : IRepository<Coupon>
    {
        void update(Coupon coupon);


    }
}
