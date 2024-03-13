using Eshop.Data.Data;
using Eshop.Model.Models;

namespace Eshop.Data.Repository
{
    public class OrderDetailsRepository:Repository<OrderDetails>,IOrderDetailsRepository
    {
        private readonly ApplicationDbContext context;

        public OrderDetailsRepository(ApplicationDbContext context):base(context)
        {
            this.context = context;
        }

     
        public void update(OrderDetails orderDetails)
        {
            context.Update(orderDetails);
        }

        public void updateDeliveryTime(int id, DateTime deliveryTime)
        {
            var orderFromDb = context.orderDetails.FirstOrDefault(x => x.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.DeliveredDate = deliveryTime;

            }
        }

        public void UpdatePaymentStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var orderFromDb = context.orderDetails.FirstOrDefault(x => x.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.ProductStatus = orderStatus;
                if (!string.IsNullOrEmpty(paymentStatus))
                {
                    orderFromDb.ProductPaymentStatus = paymentStatus;
                }
            }
        }

        public void UpdateStatus(int id, string ProductStatus)
        {
            var orderFromDb = context.orderDetails.FirstOrDefault(x => x.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.ProductStatus = ProductStatus;
                
            }
        }
    }
}
