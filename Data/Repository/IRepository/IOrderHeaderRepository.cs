using Eshop.Model.Models;
using Eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Repository
{
    public interface IOrderHeaderRepository:IRepository<OrderHeader>
    {
        void update(OrderHeader orderHeader);
        void UpdateStatus(int id,string orderStatus,string? paymentStatus=null);
        void UpdateStripePaymentId(int id,string sessionID, string paymentIntentId);
   
        void updateDeliveryTime(int id,DateTime deliveryTime);
    }
}
