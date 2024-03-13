using Eshop.Data.Data;
using Eshop.Data.Repository;
//using Eshop.Migrations;
using Eshop.Model.Models;
using Eshop.Models;
using Eshop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Climate;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Eshop.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitofWork unitofWork;
        private readonly ApplicationDbContext context;

        [BindProperty]
        public Test test {  get; set; } 

        public OrderController(IUnitofWork unitofWork,ApplicationDbContext context)
        {
            this.unitofWork = unitofWork;
            this.context = context;
        }
        public IActionResult CancelOrder(int Id, string newStatus)
        {
            var order = unitofWork.OrderHeader.Get(c => c.Id == Id);

            if (order == null)
            {
                return NotFound(); // Order not found
            }

            if (order.PaymentStatus == SD.PaymentStatusApproved)
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
                unitofWork.OrderHeader.UpdateStatus(Id, SD.statusCancelled, SD.statusCancelled);
                unitofWork.Save();
            }
            TempData["successMessage"] = "Order Cancelled Succesfully";
            List<OrderHeader> orderHeaders = unitofWork.OrderHeader.GetAll(includeProperties: "applicationUser").ToList();

            return RedirectToAction("UserOrders"); // Return success status
        }
        public IActionResult UserOrders()
        {
            var claimsIdentity = (ClaimsIdentity?)User.Identity;
            var UserId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var orders = unitofWork.OrderHeader.GetAll(u => u.UserId == UserId, includeProperties: "orderDetails,orderDetails.products,orderDetails.products.productImages");
            var orderIds = orders.Select(o => o.Id).ToList();
            var orderDetails = unitofWork.OrderDetails.GetAll(od => orderIds.Contains(od.OrderId), includeProperties: "products,products.productImages");

            foreach(var order in orderDetails)
            {
                if (order.ProductStatus == SD.statusDelivered)
                {
                    var deliveredDate = order.DeliveredDate;
                    var date25DaysAgo = deliveredDate.AddDays(25);
                    var currentDate = DateTime.Now;
                    ViewBag.returnDateFinish = date25DaysAgo;
                    if (currentDate <= date25DaysAgo)
                    {
                        order.ReturnPolicyValid = true;
                    }
                    else
                    {
                        // Set a flag indicating that the return policy has expired for this order
                        order.ReturnPolicyValid = false;
                    }
                    unitofWork.OrderDetails.update(order);
                    unitofWork.Save();
                }
            }

          

            return View(orders.ToList());
        }
	    public IActionResult Return(int? productId,int? orderDetailsId,int? orderHeaderId)
		{
            

            var productToReturn = unitofWork.OrderDetails.Get(u => u.Id == orderDetailsId);
            if(productToReturn== null)
            {
                return NotFound();
            }
            if (productToReturn != null)
            {
                productToReturn.IsReturned= true;
                unitofWork.OrderDetails.update(productToReturn);
                unitofWork.Save();

              
                var product =unitofWork.Product.Get(u=>u.Id== productId);
                product.StockLeft += productToReturn.Count;
                
                unitofWork.Product.updateStock(product);
                unitofWork.Save();
                var orderHeader=unitofWork.OrderHeader.Get(u=>u.Id== orderHeaderId,includeProperties: "orderDetails");
                if (orderHeader != null)
                {
                   var entityEntry = context.Entry(productToReturn);
                    entityEntry.State = EntityState.Detached;
                }
                var productPrice = productToReturn.Price; // Assuming this is the price per unit
                orderHeader.OrderTotal-= productPrice;
                unitofWork.OrderHeader.update(orderHeader);
                unitofWork.Save();
				if (orderHeader.isShipped)
				{
					productPrice += SD.ShippingCharge / orderHeader.orderDetails.Count;
				}
				if (productToReturn.ProductPaymentStatus == SD.PaymentStatusApproved)
                {
                    var userId = IdentityHelper.GetUserId(User);
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
							TransactionType = SD.TransactionReturnRefund
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
							TransactionType = SD.TransactionReturnRefund
						};
						unitofWork.Wallet.Add(wallett);
						unitofWork.Save();
						TempData["successMessage"] = "The refunded amount has been credited to the user's wallet.";
					}

					//string paymentIntentId = orderHeader.PaymentIntendId; // This is just an example; you need to obtain the correct payment intent ID

					//// Create a refund for that specific product using the payment intent ID associated with it
					//var options = new RefundCreateOptions
					//{
					//    Reason = RefundReasons.RequestedByCustomer,
					//    PaymentIntent = paymentIntentId,
					//    Amount = (long)(productPrice* 100), // Assuming the price is in USD and converted to cents
					//};

					//var service = new RefundService();
					//Refund refund = service.Create(options);
					unitofWork.OrderDetails.UpdatePaymentStatus(productToReturn.Id, SD.statusReturned, SD.PaymentStatusRefunded);


                }
                else
                {
                    unitofWork.OrderDetails.UpdatePaymentStatus(productToReturn.Id, SD.statusReturned, SD.PaymentStatusClosed);

                }

                unitofWork.Save();
                bool remainingItems = orderHeader.orderDetails.Any(od => od.Id != productToReturn.Id);

                if (remainingItems)
                {
                    unitofWork.OrderHeader.UpdateStatus(orderHeader.Id, SD.statuspartiallyReturned, SD.PaymentStatusPartialRefunded);

                }
                else
                {
                    unitofWork.OrderHeader.UpdateStatus(orderHeader.Id, SD.statusReturned, SD.PaymentStatusRefunded);

                }
                unitofWork.Save();
            }
            TempData["successMessage"] = "Order Returned Succesfully";
            List<OrderHeader> orderHeaders = unitofWork.OrderHeader.GetAll(includeProperties: "applicationUser").ToList();

            return RedirectToAction("UserOrders", orderHeaders); // Return success status
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
                if (orderHeader.isShipped)
                {
                    productPrice += SD.ShippingCharge / orderHeader.orderDetails.Count;
                }
                unitofWork.OrderHeader.update(orderHeader);
                unitofWork.Save();
                if (orderToCancel.ProductPaymentStatus == SD.PaymentStatusApproved)
                {
                    if (orderHeader.PaymentType == SD.onlinePayment)
                    {
                        string userId = IdentityHelper.GetUserId(User);
                        var walletfound = unitofWork.WalletHeader.Get(u=>u.UserId== userId);

                        if (walletfound == null)
                        {
							

							WalletHeader walletHead = new WalletHeader()
						    {
							UserId= userId,
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
                            walletfound.Balance+=productPrice;
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
                     
                       
						//string paymentIntentId = orderHeader.PaymentIntendId; // This is just an example; you need to obtain the correct payment intent ID

						//// Create a refund for that specific product using the payment intent ID associated with it
						//var options = new RefundCreateOptions
						//{
						//    Reason = RefundReasons.RequestedByCustomer,
						//    PaymentIntent = paymentIntentId,
						//    Amount = (long)(productPrice * 100), // Assuming the price is in USD and converted to cents
						//};

						//var service = new RefundService();
						//Refund refund = service.Create(options);

					}

                    unitofWork.OrderDetails.UpdatePaymentStatus(orderToCancel.Id, SD.statusCancelled, SD.PaymentStatusRefunded);
                }
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
            }
            TempData["successMessage"] = "Order Cancelled Succesfully";
            List<OrderHeader> orderHeaders = unitofWork.OrderHeader.GetAll(includeProperties: "applicationUser").ToList();

            return RedirectToAction("UserOrders", orderHeaders); // Return success status

        }
        public IActionResult Test()
        {
            return View();
        }
        //public IActionResult InitiateOrder()
        //{
        //    string key = "rzp_test_r4LDjgOMqBZxwZ";
        //    string secret = "3ga4krs17WidizLYDAs02oh7";
        //    Random random = new Random();
        //    string TransactionId=random.Next(0,10000).ToString();

        //    Dictionary<string, object> input = new Dictionary<string, object>();
        //    input.Add("amount",Convert.ToDecimal(test.TotalAmount)*100); // this amount should be same as transaction amount
        //    input.Add("currency", "INR");
        //    input.Add("receipt", TransactionId);



        //    RazorpayClient client = new RazorpayClient(key, secret);

        //    Razorpay.Api.Order order = client.Order.Create(input);
        //    ViewBag.OrderId = order["id"].ToString();
        //    return View("Payment",test);

        //}
        //public IActionResult Payment(string razorpay_payment_id, string razorpay_order_id,string razorpay_signature)
        //{

        //    Dictionary<string,string> attributes=new Dictionary<string,string>();
        //    attributes.Add("razorpay_payment_id", razorpay_payment_id);
        //    attributes.Add("razorpay_order_id", razorpay_order_id);

        //    attributes.Add("razorpay_signature", razorpay_signature);
        //    Utils.verifyPaymentSignature(attributes);
        //    Test test = new Test();
        //    test.TransactionId=razorpay_payment_id;
        //    test.OrderId=razorpay_order_id;

        //    return View("PaymentSuccess",test);

        //}
    }
}
