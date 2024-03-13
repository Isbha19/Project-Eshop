using Eshop.Model.Models;
using Eshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Repository
{
    public interface IOrderDetailsRepository:IRepository<OrderDetails>
    {
        void update(OrderDetails orderDetails);

        void UpdateStatus(int id, string orderStatus);

        void updateDeliveryTime(int id, DateTime deliveryTime);
        void UpdatePaymentStatus(int id, string orderStatus, string? paymentStatus = null);

    }
}
