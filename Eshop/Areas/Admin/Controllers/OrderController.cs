
using Eshop.Data.Data;
using Eshop.Data.Repository;
using Eshop.Model.Models;
using Eshop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Climate;
using System.Security.Claims;
namespace Eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class OrderController : Controller
    {



        private readonly IUnitofWork unitofWork;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ApplicationDbContext context;

        public OrderController(IUnitofWork unitofWork, IWebHostEnvironment webHostEnvironment, ApplicationDbContext context)
        {


            this.unitofWork = unitofWork;
            this.webHostEnvironment = webHostEnvironment;
            this.context = context;
        }

        public IActionResult Index()
        {
            try
            {
                List<OrderHeader> orderHeaders = unitofWork.OrderHeader.GetAll(includeProperties: "applicationUser").ToList();
                foreach (var order in orderHeaders) {
                if(order.OrderTotal==0)
                    {
                        unitofWork.OrderHeader.UpdateStatus(order.Id, "Order Closed", "Payment closed");
                        unitofWork.Save();
                    }
                
                }
                return View(orderHeaders);

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Something went wrong in displaying orders" + ex.Message;
                return View();
            }

        }
        public IActionResult ViewOrder(int? Id)
        {

            OrderHeader order = unitofWork.OrderHeader.Get(u => u.Id == Id, includeProperties: "orderDetails,orderDetails.products");
          
            return View(order);

        }
        public IActionResult UpdateOrderStatus(int Id, string newStatus)
        {
            var order = unitofWork.OrderHeader.Get(c => c.Id == Id);

            if (order == null)
            {
                return NotFound(); // Order not found
            }

            // Update the order status
            order.OrderStatus = newStatus;
            if (order.OrderStatus == SD.statusDelivered)
            {
                order.DeliveredDate = DateTime.Now;
                unitofWork.OrderHeader.updateDeliveryTime(Id, order.DeliveredDate);
                if (order.PaymentType == "cash")
                {
                    order.PaymentStatus = SD.PaymentStatusApproved;
                    unitofWork.OrderHeader.UpdateStatus(Id, newStatus, order.PaymentStatus);
                }
                else
                {
                    unitofWork.OrderHeader.UpdateStatus(Id, newStatus);

                }
                unitofWork.Save();

            }
            else
            {
                unitofWork.OrderHeader.UpdateStatus(Id, newStatus);
                unitofWork.Save();
            }


            return Ok(); // Return success status
        }

        public IActionResult UpdateProductStatus(int Id, string newStatus)
        {
            var ProductDetail = unitofWork.OrderDetails.Get(c => c.Id == Id);

            if (ProductDetail == null)
            {
                return NotFound(); // Order not found
            }

            // Update the order status
            ProductDetail.ProductStatus = newStatus;
            if (ProductDetail.ProductStatus == SD.statusDelivered)
            {
                ProductDetail.DeliveredDate = DateTime.Now;
                unitofWork.OrderDetails.updateDeliveryTime(Id, ProductDetail.DeliveredDate);
                var orderHeader = unitofWork.OrderHeader.Get(u => u.orderDetails.Any(od => od.Id == Id));
                if (orderHeader.PaymentType == SD.cashOnDelivery)
                {
                    unitofWork.OrderDetails.UpdatePaymentStatus(Id, SD.statusDelivered, SD.PaymentStatusApproved);
                    unitofWork.OrderHeader.UpdateStatus(orderHeader.Id, SD.statusApproved, SD.PaymentStatusApproved);
                }
            }
            unitofWork.OrderDetails.UpdateStatus(Id, newStatus);
            unitofWork.Save();


            return Ok(); // Return success status
        }
        public IActionResult CancelOrder(int Id)
        {
            var order = unitofWork.OrderHeader.Get(c => c.Id == Id);

            if (order == null)
            {
                return NotFound();
            }

            if (order.PaymentStatus == SD.PaymentStatusApproved)
            {
                if (order.PaymentType == "card")
                {
                    var options = new RefundCreateOptions
                    {
                        Reason = RefundReasons.RequestedByCustomer,
                        PaymentIntent = order.PaymentIntendId
                    };
                    var service = new RefundService();
                    Refund refund = service.Create(options);

                    unitofWork.OrderHeader.UpdateStatus(Id, SD.statusCancelled, SD.PaymentStatusRefunded);
                    unitofWork.Save();
                }
                else
                {
                    unitofWork.OrderHeader.UpdateStatus(Id, SD.statusCancelled, SD.PaymentStatusRefunded);
                    unitofWork.Save();
                }
            }
            else
            {
                unitofWork.OrderHeader.UpdateStatus(Id, SD.statusCancelled, SD.statusCancelled);
                unitofWork.Save();
            }
            TempData["successMessage"] = "Order Cancelled Succesfully";
            List<OrderHeader> orderHeaders = unitofWork.OrderHeader.GetAll(includeProperties: "applicationUser").ToList();

            return RedirectToAction("Index", orderHeaders); // Return success status
        }



        public IActionResult CancelSingleOrder(int? Id)
        {
            var orderToCancel = unitofWork.OrderDetails.Get(c => c.Id == Id);

            if (orderToCancel == null)
            {
                return NotFound();
            }
            if (orderToCancel != null)
            {
                orderToCancel.ProductStatus = SD.statusCancelled;
                unitofWork.OrderDetails.update(orderToCancel);
                unitofWork.Save();


                var product = unitofWork.Product.Get(u => u.Id == orderToCancel.ProductId);
                product.StockLeft += orderToCancel.Count;

                unitofWork.Product.updateStock(product);
                unitofWork.Save();
                var orderHeader = unitofWork.OrderHeader.Get(u => u.orderDetails.Any(od => od.Id == orderToCancel.Id), includeProperties: "orderDetails");
                if (orderHeader != null)
                {
                    var entityEntry = context.Entry(orderToCancel);
                    entityEntry.State = EntityState.Detached;
                }
                var productPrice = orderToCancel.Price; // Assuming this is the price per unit
                orderHeader.OrderTotal -= productPrice;
                unitofWork.OrderHeader.update(orderHeader);
                unitofWork.Save();
				if (orderHeader.isShipped)
				{
					productPrice += SD.ShippingCharge / orderHeader.orderDetails.Count;
				}


				if (orderToCancel.ProductPaymentStatus == SD.PaymentStatusApproved)
                {
                    string userId = orderHeader.UserId;
					var walletfound = unitofWork.WalletHeader.Get(u => u.UserId == userId);
					if (walletfound == null)
					{
						WalletHeader walletHead = new WalletHeader()
						{
							UserId = userId,
							Balance = productPrice,
						};
						unitofWork.WalletHeader.Add(walletHead);
						unitofWork.Save();
						Wallet wallett = new Wallet()
						{
							WalletId = walletHead.Id,
							Amount = productPrice,
							TransactionDate = DateTime.Now,
							TransactionType = SD.TransactionCancelRefund
						};
						unitofWork.Wallet.Add(wallett);
						unitofWork.Save();
						TempData["successMessage"] = "The refunded amount has been credited to the user's wallet.";
					}
					else
					{
						walletfound.Balance += productPrice;
						unitofWork.WalletHeader.update(walletfound);
						unitofWork.Save();
						Wallet wallett = new Wallet()
						{
							WalletId = walletfound.Id,
							Amount = productPrice,
							TransactionDate = DateTime.Now,
							TransactionType = SD.TransactionCancelRefund
						};
						unitofWork.Wallet.Add(wallett);
						unitofWork.Save();
						TempData["successMessage"] = "The refunded amount has been credited to the user's wallet.";
					}

				}
				//if (orderHeader.PaymentType == SD.onlinePayment)
				//{

				//    string paymentIntentId = orderHeader.PaymentIntendId; // This is just an example; you need to obtain the correct payment intent ID

				//    // Create a refund for that specific product using the payment intent ID associated with it
				//    var options = new RefundCreateOptions
				//    {
				//        Reason = RefundReasons.RequestedByCustomer,
				//        PaymentIntent = paymentIntentId,
				//        Amount = (long)(productPrice * 100), // Assuming the price is in USD and converted to cents
				//    };

				//    var service = new RefundService();
				//    Refund refund = service.Create(options);

				//}

				//unitofWork.OrderDetails.UpdatePaymentStatus(orderToCancel.Id, SD.statusCancelled, SD.PaymentStatusRefunded);

				else
                {
                    unitofWork.OrderDetails.UpdatePaymentStatus(orderToCancel.Id, SD.statusCancelled, SD.PaymentStatusClosed);

                }
                unitofWork.Save();
                bool remainingItems = orderHeader.orderDetails.Any(od => od.Id != orderToCancel.Id);

                if (!remainingItems)
                {
                    unitofWork.OrderHeader.UpdateStatus(orderHeader.Id, SD.statusCancelled, SD.PaymentStatusRefunded);

                }

                unitofWork.Save();
				TempData["successMessage"] = "Order Cancelled Succesfully";
				//List<OrderHeader> orderHeaders = unitofWork.OrderHeader.GetAll(includeProperties: "applicationUser,orderDetails,orderDetails.products").ToList();
				var orderHead = unitofWork.OrderHeader.Get(u => u.orderDetails.Any(od => od.Id == orderToCancel.Id), includeProperties: "orderDetails,orderDetails.products");

				return View("ViewOrder", orderHead); // Return success status
			}

            return View("ViewOrder");
        }
    }
}
