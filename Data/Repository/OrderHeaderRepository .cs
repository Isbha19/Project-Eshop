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
    public class OrderHeaderRepository:Repository<OrderHeader>,IOrderHeaderRepository
    {
        private readonly ApplicationDbContext context;

        public OrderHeaderRepository(ApplicationDbContext context):base(context)
        {
            this.context = context;
        }

     
        public void update(OrderHeader orderHeader)
        {
            context.Update(orderHeader);
        }

        public void updateDeliveryTime(int id, DateTime deliveryDate)
        {
            var orderFromDb = context.orderHeaders.FirstOrDefault(x => x.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.DeliveredDate = deliveryDate;
               
            }
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var orderFromDb = context.orderHeaders.FirstOrDefault(x => x.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;
                if (!string.IsNullOrEmpty(paymentStatus))
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
        }

        public void UpdateStripePaymentId(int id, string sessionID, string paymentIntentId)
		{
			var orderFromDb = context.orderHeaders.FirstOrDefault(x => x.Id == id);
            if (!string.IsNullOrEmpty(sessionID))
            {
                orderFromDb.SessionId= sessionID;

            }
            if (!string.IsNullOrEmpty(paymentIntentId))
            {
                orderFromDb.PaymentIntendId= paymentIntentId;
                orderFromDb.PaymentTime = DateTime.Now;
            }
		}
	}
}
