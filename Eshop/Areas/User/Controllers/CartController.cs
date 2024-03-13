using Eshop.Data.Data;
using Eshop.Data.Repository;
using Eshop.Model.Models;
using Eshop.Model.ViewModels;
using Eshop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using DinkToPdf;
using System.Security.Claims;
using System.Text;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Eshop.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitofWork unitofWork;
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IConverter pdfConverter;
        private readonly IConfiguration configuration;
        private readonly IEmailSender emailSender;

        public CartController(IUnitofWork unitofWork, ApplicationDbContext context,IWebHostEnvironment webHostEnvironment, IConverter pdfConverter, IConfiguration configuration, IEmailSender emailSender)
        {
            this.unitofWork = unitofWork;
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
            this.pdfConverter = pdfConverter;
            this.configuration = configuration;
            this.emailSender = emailSender;
        }

        //Cart Page
        public IActionResult Cart(int? Id, bool? FromWishlist)
        {
			var cartItem = unitofWork.Cart.GetAll(u => u.UserId == IdentityHelper.GetUserId(User));
			if (cartItem != null)
			{
				HttpContext.Session.SetString("CartCount", cartItem.Count().ToString());
			}
			else
			{
				HttpContext.Session.SetString("CartCount", 0.ToString());
			}
			var UserId = getUserId();

            if (Id != null)
            {
                var prod = unitofWork.Cart.Get(c => c.productId == Id & c.UserId == UserId);
                if (prod != null)
                {
                    TempData["errorMessage"] = "Product Already in Cart";
                }
                else
                {
                    var product = unitofWork.Product.Get(c => c.Id == Id);
                    ShoppingCart cart = new ShoppingCart()
                    {
                        productId = product.Id,
                        UserId = UserId,
                        Count = 1

                    };
                    unitofWork.Cart.Add(cart,TempData);
                    unitofWork.Save();
                    TempData["successMessage"] = "Product Added to Cart";

                }

            }
            double OrderTotalAmt = getOrderTotalAmount();
            var orderViewModel = CreateOrderViewModel(OrderTotalAmt, UserId, FromWishlist, Id);
            
            return View(orderViewModel);
        }
        //order summary
        public IActionResult OrderSummary(string? selectedCouponCode)
        {
            var UserId = getUserId();
            var user = unitofWork.User.Get(u => u.Id == UserId);
            if (selectedCouponCode != null && selectedCouponCode == SD.RefferalFirstOrder)
            {
                user.isRefferalFlag = false;
                unitofWork.User.update(user);
                unitofWork.Save();
            }


            double OrderTotalAmt = getOrderTotalAmount();
            var orderViewModel = CreateOrderViewModel(OrderTotalAmt, UserId);
            if (orderViewModel.ShippingAdresses.Count() == 0)
            {
                TempData["errorMessage"] = "Kindly provide us with your shipping address details for a smooth delivery process.";
                return RedirectToAction("Cart");

            }
            return View(orderViewModel);
        }

      

        //choose payment
        public IActionResult ChoosePayment(double paramTotal, double walletBalance)
        {
            OrderViewModel orderViewModel = new OrderViewModel();
            HttpContext.Session.SetString("ParamTotal", paramTotal.ToString());
            HttpContext.Session.SetString("WalletBalance", walletBalance.ToString());
            return View(orderViewModel);
        }

      //choose payment post
        [HttpPost]
        public IActionResult ChoosePayment(OrderViewModel? vieworder)
        {
            var UserId = getUserId();
            var user = unitofWork.User.Get(u => u.Id == UserId);
            if (user.LockoutEnd != null)
            {
                TempData["errorMessage"] = "Unfortunately, we can't take your order right now because your account is currently blocked.";
                return View(vieworder);

            }

            double OrderTotalAmt = getOrderTotalAmount();
            var orderViewModel = CreateOrderViewModel(OrderTotalAmt, UserId);
          
            orderViewModel.orderHeader=new OrderHeader();
            orderViewModel.orderHeader.UserId = UserId;
            orderViewModel.orderHeader.Name = orderViewModel.ShippingAdresses.FirstOrDefault(sa => sa.IsDefault)?.Name;
            orderViewModel.orderHeader.PhoneNumber = orderViewModel.ShippingAdresses.FirstOrDefault(sa => sa.IsDefault)?.Phone;
            orderViewModel.orderHeader.Address = orderViewModel.ShippingAdresses.FirstOrDefault(sa => sa.IsDefault)?.Address;
            orderViewModel.orderHeader.City = orderViewModel.ShippingAdresses.FirstOrDefault(sa => sa.IsDefault)?.City;
            orderViewModel.orderHeader.State = orderViewModel.ShippingAdresses.FirstOrDefault(sa => sa.IsDefault)?.State;
            orderViewModel.orderHeader.Pincode = orderViewModel.ShippingAdresses.FirstOrDefault(sa => sa.IsDefault)?.PinCode;
            orderViewModel.orderHeader.saveAddress = orderViewModel.ShippingAdresses.FirstOrDefault(sa => sa.IsDefault)?.saveAddress;
            orderViewModel.orderHeader.OrderTotal = orderViewModel.OrderTotal;

            if (getCartList(UserId).Sum(item=>item.CouponDiscountPrice) > 0)
            {
                orderViewModel.orderHeader.DiscountAmountApplied = orderViewModel.SavedPrice;
            }
            else if (orderViewModel.offered == true)
            {
                foreach (var item in getCartList(UserId))
                {
                    if (item.products.isOffered == true)
                    {
                        orderViewModel.orderHeader.DiscountAmountApplied = orderViewModel.SavedPrice;
                    }
                }

            }


            orderViewModel.orderHeader.OrderStatus = SD.statusPending;
            orderViewModel.orderHeader.PaymentStatus = SD.PaymentStatusPending;
            orderViewModel.orderHeader.OrderDate = DateTime.Today;

            if (vieworder.orderHeader == null || vieworder.orderHeader.PaymentType == null)
            {
                TempData["errorMessage"] = "Select your preferred payment method";

                return View(orderViewModel);
            }
            else
            {
                orderViewModel.orderHeader.PaymentType = vieworder.orderHeader.PaymentType;

            }
            if (orderViewModel.orderHeader.PaymentType == SD.cashOnDelivery)
            {
                if (orderViewModel.OrderTotal > 1000)
                {
                    TempData["errorMessage"] = "For orders exceeding Rs 1000, we kindly request payment via methods other than cash on delivery.";

                    return View(orderViewModel);
                }
            }
            if (orderViewModel.ShipCharge > 0)
            {
                orderViewModel.orderHeader.isShipped = true;
            }
           
           



            unitofWork.OrderHeader.Add(orderViewModel.orderHeader, TempData);
            unitofWork.Save();
            foreach (var item in getCartList(UserId))
            {
                OrderDetails orderDetails = new OrderDetails()
                {
                    ProductId = item.productId,
                    OrderId = orderViewModel.orderHeader.Id,
                    Count = item.Count,
                };
                if (orderViewModel.offered)
                {
                    if (item.products.isOffered)
                    {
                        orderDetails.Price = item.CartItemOfferPrice;
                        orderDetails.discountSavedPrice = item.products.ProductPrice * item.Count - item.CartItemOfferPrice;
                    }
                    else
                    {
                        orderDetails.Price = item.TotalPrice;
                        orderDetails.discountSavedPrice = 0;
                    }
                }
                else if (item.CouponApplied)
                {
                    orderDetails.Price = item.CouponDiscountPrice;
                    orderDetails.discountSavedPrice = item.products.ProductPrice * item.Count - item.CouponDiscountPrice;
                }
                else
                {
                    orderDetails.Price = item.TotalPrice;
                }
                unitofWork.OrderDetails.Add(orderDetails, TempData);


                unitofWork.Save();
                var product = unitofWork.Product.Get(p => p.Id == item.productId);
                if (product != null)
                {
                    product.OrderCount += item.Count;
                    unitofWork.Product.updateOrderCounts(product);
                    unitofWork.Save();
                }


            }
            if (orderViewModel.orderHeader.PaymentType == SD.cashOnDelivery)
            {
                return RedirectToAction("OrderConfirmation", new { Id = orderViewModel.orderHeader.Id });
            }
            if (orderViewModel.orderHeader.PaymentType == SD.walletPayment)
            {
                WalletHeader WalletHead = unitofWork.WalletHeader.Get(u => u.UserId == UserId);
                if (WalletHead.Balance >= orderViewModel.orderHeader.OrderTotal)
                {
                    WalletHead.Balance -= orderViewModel.orderHeader.OrderTotal;
                    unitofWork.WalletHeader.update(WalletHead);
                    unitofWork.Save();
                    Wallet wallet = new Wallet()
                    {
                        WalletId = WalletHead.Id,
                        Amount = orderViewModel.orderHeader.OrderTotal,
                        TransactionDate = DateTime.Now,
                        TransactionType = SD.TransactionWithDraw
                    };
                    unitofWork.Wallet.Add(wallet, TempData);
                    unitofWork.Save();          
                    return RedirectToAction("OrderConfirmation", new { Id = orderViewModel.orderHeader.Id });

                }
                else
                {
                    TempData["errorMessage"] = "You have insufficient balance in your wallet to proceed the order";
                    return View(orderViewModel);
                }


            }

            var domain = "https://localhost:7088/";

            var options = new Stripe.Checkout.SessionCreateOptions
            {

                SuccessUrl = domain + $"User/Cart/OrderConfirmation/?id={orderViewModel.orderHeader.Id}",
                CancelUrl = domain + "User/Cart/Cart",
                LineItems = new List<SessionLineItemOptions>(),

                Mode = "payment",



            };
            foreach (var item in orderViewModel.shoppingCarts)
            {


                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(
        item.products.isOffered
            ? Convert.ToDouble(item.CartItemOfferPrice) / Convert.ToDouble(item.Count) * 100
            : (item.CouponApplied
                ? item.CouponDiscountPrice / item.Count * 100
                : item.TotalPrice / item.Count * 100)
    ),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.products.ProductName,
                            Description = item.products.ProductDescription, // Add description if available


                        }
                    },
                    Quantity = item.Count
                };
                options.LineItems.Add(sessionLineItem);
            }
            try
            {
                var service = new Stripe.Checkout.SessionService();
                Session session = service.Create(options);
                unitofWork.OrderHeader.UpdateStripePaymentId(orderViewModel.orderHeader.Id, session.Id, session.PaymentIntentId);
                unitofWork.Save();

                Response?.Headers?.Add("Location", session.Url);
                return new StatusCodeResult(303);

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Payment above ₹ 10,000 not Allowed";
            }

            return View(orderViewModel);
        }
        public IActionResult OrderConfirmation(int Id)
        {
            OrderHeader orderHeader = unitofWork.OrderHeader.Get(u => u.Id == Id);
            if (orderHeader != null)
            {
                var orders = unitofWork.OrderDetails.GetAll(u => u.OrderId == Id);
                //updating stock left
                foreach (var item in orders)
                {
                    var product = unitofWork.Product.Get(u => u.Id == item.ProductId);
                    if (product != null)
                    {
                        product.StockLeft = product.StockLeft - item.Count;
                        unitofWork.Product.updateStock(product);
                    }
                }

                if (string.Equals(orderHeader.PaymentType, SD.onlinePayment, StringComparison.OrdinalIgnoreCase))
                {
                    var service = new SessionService();
                    Session session = service.Get(orderHeader.SessionId);
                    if (session != null && session.PaymentStatus.ToLower() == "paid")
                    {
                        unitofWork.OrderHeader.UpdateStripePaymentId(Id, session.Id, session.PaymentIntentId);
                        unitofWork.OrderHeader.UpdateStatus(Id, SD.statusApproved, SD.PaymentStatusApproved);
                        var singleorders = unitofWork.OrderDetails.GetAll(u => u.OrderId == Id);
                        if (singleorders != null)
                        {
                            foreach (var item in singleorders)
                            {
                                unitofWork.OrderDetails.UpdatePaymentStatus(item.Id, SD.statusApproved, SD.PaymentStatusApproved);
                                unitofWork.Save();
                            }
                        }

                    }
                }
                else if (orderHeader.PaymentType == SD.cashOnDelivery)
                {
                    unitofWork.OrderHeader.UpdateStatus(Id, SD.statusApproved, SD.PaymentStatusPending);
                    var singleorders = unitofWork.OrderDetails.GetAll(u => u.OrderId == Id);
                    if (singleorders != null)
                    {
                        foreach (var item in singleorders)
                        {
                            unitofWork.OrderDetails.UpdatePaymentStatus(item.Id, SD.statusApproved, SD.PaymentStatusPending);
                            unitofWork.Save();
                        }
                    }
                }
                else if (orderHeader.PaymentType == SD.walletPayment)
                {
                    unitofWork.OrderHeader.UpdateStatus(Id, SD.statusApproved, SD.PaymentStatusApproved);
                    var singleorders = unitofWork.OrderDetails.GetAll(u => u.OrderId == Id);

                    if (singleorders != null)
                    {
                        foreach (var item in singleorders)
                        {
                            unitofWork.OrderDetails.UpdatePaymentStatus(item.Id, SD.statusApproved, SD.PaymentStatusApproved);
                            unitofWork.Save();
                        }
                    }
                }

                unitofWork.Save();

                List<ShoppingCart> shoppingCarts = unitofWork.Cart.GetAll(u => u.UserId == orderHeader.UserId).ToList();
                unitofWork.Cart.DeleteRange(shoppingCarts);
                unitofWork.Save();
                var user = unitofWork.User.Get(u => u.Id == getUserId());
                emailSender.SendEmailAsync(user?.Email, "Order Confirmed",$"Your order have been confirmed with order Id {orderHeader.Id}");

                return View(Id);
            }
            else
            {
                TempData["errorMessage"] = "Order error";
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult ChangeAddress(int? selectedAddress)
        {
            var UserId = getUserId();

            if (selectedAddress != null)
            {

                var addressToChange = unitofWork.Address.Get(c => c.Id == selectedAddress);
                addressToChange.IsDefault = true;
                unitofWork.Address.update(addressToChange);
                unitofWork.Save();
                var addresses = unitofWork.Address.GetAll(c => c.UserId == UserId && c.Id != selectedAddress);
                foreach (var item in addresses)
                {
                    item.IsDefault = false;
                    unitofWork.Address.update(item);

                }
                unitofWork.Save();
            }
            double OrderTotalAmt = getOrderTotalAmount();
            var orderViewModel = CreateOrderViewModel(OrderTotalAmt, UserId);
            return View("Cart", orderViewModel);
        }


        public IActionResult Delete(int? Id)
        {
            var UserId = getUserId();
            var cartItem = unitofWork.Cart.Get(u => u.productId == Id && u.UserId == UserId);
            unitofWork.Cart.Delete(cartItem);
            unitofWork.Save();
            TempData["successMessage"] = "Product Deleted";

            var cartItems = unitofWork.Cart.GetAll(u => u.UserId == UserId, includeProperties: "products,products.productImages");
            return RedirectToAction("Cart", cartItems);

        }

        public IActionResult Plus(int? Id)
        {
            var UserId = getUserId();
            var cartItem = unitofWork.Cart.Get(u => u.productId == Id && u.UserId == UserId);
            var product = unitofWork.Product.Get(u => u.Id == Id);
            if (cartItem.Count < product.StockLeft)
            {
                if (cartItem.Count < 10) // Check if the count is less than 10
                {
                    cartItem.Count += 1;
                    unitofWork.Cart.update(cartItem);
                    unitofWork.Save();
                }
                else
                {
                    TempData["ErrorCartMessage"] = "Sorry, you can only purchase up to 10 items at a time.";
                }
            }
            else
            {
                TempData["ErrorCartMessage"] = "No stocks Left";

            }



            var cartItems = unitofWork.Cart.GetAll(u => u.UserId == UserId, includeProperties: "products,products.productImages");

            return RedirectToAction("Cart", cartItems);

        }
        public IActionResult Minus(int? Id)
        {
            var UserId =getUserId();

            var cartItem = unitofWork.Cart.Get(u => u.productId == Id);
            if (cartItem.Count > 1)
            {
                cartItem.Count -= 1;
                unitofWork.Cart.update(cartItem);
                unitofWork.Save();
            }

            var cartItems = getCartList(UserId);
            return RedirectToAction("Cart", cartItems);

        }

        [HttpGet]
        public IActionResult DownloadInvoice(int? orderId)
        {
            try
            {

                // Retrieve order details based on orderId
                var orderDetails = unitofWork.OrderDetails.GetAll(od => od.OrderId == orderId, includeProperties: "products,orderHeader").ToList();

                // Generate HTML content for the invoice
                string htmlContent = GenerateHtmlInvoice(orderDetails);

                // Convert HTML to PDF
                var pdfBytes = ConvertHtmlToPdf(htmlContent);

                if (pdfBytes != null)
                {
                    // Set the appropriate content type for PDF
                    string contentType = "application/pdf";


                    // Return the PDF file as a FileResult
                    return File(pdfBytes, contentType, "invoice.pdf");
                }
                else
                {
                    TempData["errorMessage"] = "Failed to generate invoice PDF.";
                    return RedirectToAction("OrderConfirmation", "Cart", new { Id = orderId });
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "An error occurred while generating the invoice PDF.";
                // Log the exception or handle it accordingly
                return RedirectToAction("OrderConfirmation", "Cart", new { Id = orderId });
            }
        }



        private string GenerateHtmlInvoice(List<OrderDetails> orderDetails)
        {
            StringBuilder htmlContent = new StringBuilder();
            htmlContent.AppendLine("<!DOCTYPE html>");
            htmlContent.AppendLine("<html>");
            htmlContent.AppendLine("<head>");
            htmlContent.AppendLine("<title>Invoice</title>");
            htmlContent.AppendLine("<style>");
            htmlContent.AppendLine("body { font-family: Arial, sans-serif; text-align: center; }");
            htmlContent.AppendLine("h1 { color: #333; }");
            htmlContent.AppendLine("h2 { color: #666; }");
            htmlContent.AppendLine("table { width: 100%; border-collapse: collapse; }");
            htmlContent.AppendLine("th, td { border: 1px solid #ccc; padding: 8px; }");
            htmlContent.AppendLine("tr:nth-child(even) { background-color: #f2f2f2; }");
            htmlContent.AppendLine(".total-row { font-weight: bold; }");
            htmlContent.AppendLine(".address { text-align: left; }"); // Align address to the left

            htmlContent.AppendLine("</style>");
            htmlContent.AppendLine("</head>");
            htmlContent.AppendLine("<body>");
            htmlContent.AppendLine("<h1>Invoice</h1>");
            htmlContent.AppendLine("<hr />");
            htmlContent.AppendLine("<h2>Order Details</h2>");
            htmlContent.AppendLine("<table>");
            htmlContent.AppendLine("<tr><th>Product</th><th>Quantity</th><th>Price</th></tr>");

            foreach (var orderDetail in orderDetails)
            {
                htmlContent.AppendLine($"<tr><td>{orderDetail?.products?.ProductName}</td><td>{orderDetail?.Count}</td><td>₹{orderDetail?.Price}</td></tr>");
            }
            if (orderDetails != null && orderDetails.Any() && orderDetails.FirstOrDefault()?.orderHeader != null)
            {
                var firstOrderHeader = orderDetails.FirstOrDefault().orderHeader;

                if (firstOrderHeader.isShipped)
                {
                    htmlContent.AppendLine("<tr><td colspan=\"2\">Shipping Charge</td><td>₹40</td></tr>");


                }
            }

            double totalPrice = 0;
            // Calculate total price
            if (orderDetails.FirstOrDefault().orderHeader.isShipped)
            {
                totalPrice = orderDetails.Sum(od => od.Price * od.Count) + SD.ShippingCharge;

            }
            else
            {
                totalPrice = orderDetails.Sum(od => od.Price * od.Count);

            }
            htmlContent.AppendLine($"<tr><td colspan=\"2\">Total Price</td><td>₹{totalPrice}</td></tr>");
            htmlContent.AppendLine("</table>");


            // Include payment status
            htmlContent.AppendLine($"<p>Payment Status: {orderDetails.FirstOrDefault()?.orderHeader?.PaymentStatus}</p>");

            // Address
            htmlContent.AppendLine("<h2 class=\"address\">Address</h2>");
            htmlContent.AppendLine("<p class=\"address\">");
            htmlContent.AppendLine($"Address: {orderDetails.FirstOrDefault()?.orderHeader?.Address}<br/>");
            htmlContent.AppendLine($"City: {orderDetails.FirstOrDefault()?.orderHeader?.City}<br/>");
            htmlContent.AppendLine($"State: {orderDetails.FirstOrDefault()?.orderHeader?.State}<br/>");
            htmlContent.AppendLine($"Postal Code: {orderDetails.FirstOrDefault()?.orderHeader?.Pincode}<br/>");
            htmlContent.AppendLine($"Phone Number: {orderDetails.FirstOrDefault()?.orderHeader?.PhoneNumber}<br/>");

            htmlContent.AppendLine("</p>");

            htmlContent.AppendLine("</body>");
            htmlContent.AppendLine("</html>");


            return htmlContent.ToString();
        }

        private byte[] ConvertHtmlToPdf(string htmlContent)
        {
            var globalSettings = new GlobalSettings();




            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "css", "styles.css") },
                HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontSize = 9, Line = true, Center = "Invoice" }
            };

            var pdf = pdfConverter.Convert(new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            });

            return pdf;
        }
        private double getOrderTotalAmount()
        {
            var UserId =getUserId();
            var cartList = getCartList(UserId);
            double OrderTotalAmt = 0;
            int stockTotals = 0;
            double CouponedTotal = 0;
            double savedMoney = 0;
            double originalPrice = 0;
            foreach (var item in cartList)
            {

                item.TotalPrice = item.Count * item.products.ProductPrice;

                unitofWork.Cart.update(item);
                unitofWork.Save();
                originalPrice += item.TotalPrice;
                //Offer discount
                if (item.products.isOffered)
                {
                    var savAmt = item.products.ProductPrice - item.products.OfferPrice;
                    var offerName = item.products.OfferName;
                    Offer offer = unitofWork.Offer.Get(u => u.OfferName == offerName);

                    if (offer != null)
                    {
                        if (offer.offerType.ToString() == "FixedAmount")
                        {
                            item.CartItemOfferPrice = item.TotalPrice - savAmt;
                            unitofWork.Cart.update(item);
                            unitofWork.Save();

                        }
                        else if (offer.offerType.ToString() == "Percentage")
                        {

                            var savedPercent = (offer.Discount / 100) * item.TotalPrice;
                            item.CartItemOfferPrice = Convert.ToDouble(item.TotalPrice) - savedPercent;
                            unitofWork.Cart.update(item);
                            unitofWork.Save();
                        }

                    }
                    OrderTotalAmt += item.CartItemOfferPrice;
                    savedMoney += item.products.ProductPrice * item.Count - item.CartItemOfferPrice;

                }
                //coupon discount
                else if (item.CouponApplied && item.products.isOffered == false)
                {

                    CouponedTotal += item.CouponDiscountPrice;
                    savedMoney += item.TotalPrice - item.CouponDiscountPrice;
                }
                if (item.products.StockLeft != 0 && item.products.isOffered == false && item.CouponApplied == false)
                {
                    OrderTotalAmt += item.TotalPrice;

                }

                //if full stock!=0, only need for displaying all other things like address and all
                stockTotals += item.products.StockLeft;


                if (CouponedTotal > 0)
                {
                    OrderTotalAmt = CouponedTotal;

                }
                if (OrderTotalAmt < 300)
                {
                    OrderTotalAmt += 40;
                }


            }
            return OrderTotalAmt;
        }
        private List<ShoppingCart> getCartList(string UserId)
        {

            return unitofWork.Cart.GetAll(u => u.UserId == UserId, includeProperties: "products,products.productImages").ToList();

        }
        private OrderViewModel CreateOrderViewModel(double OrderTotalAmt, string UserId, bool? FromWishlist=null, int? Id=null)
        {
            var coupons = unitofWork.Coupon.GetAll();
            var shippAddress = unitofWork.Address.GetAll(c => c.UserId == UserId);
            var WalletHead = unitofWork.WalletHeader.Get(u => u.UserId == UserId);
            var user = unitofWork.User.Get(u => u.Id == UserId);
            bool Offered = getCartList(UserId).Any(item => item.CartItemOfferPrice > 0);
            bool CouponApplied = getCartList(UserId).Any(item => item.CouponApplied);
            double savedMoney = 0;
            int shipCharge = OrderTotalAmt < SD.freeShippingAmount ? SD.ShippingCharge : 0;
            foreach (var item in getCartList(UserId))
            {
                if (item.CartItemOfferPrice > 0)
                {
                    savedMoney += item.products.ProductPrice * item.Count - item.CartItemOfferPrice;
                }
                else if (item.CouponApplied)
                {
                    savedMoney += item.TotalPrice - item.CouponDiscountPrice;
                }
            }
            OrderViewModel orderViewModel = new OrderViewModel()
            {
                shoppingCarts = getCartList(UserId),
                ShippingAdresses = shippAddress,
                OrderTotal = OrderTotalAmt,
                stockTotal = getCartList(UserId).Sum(item => item.products.StockLeft),
                coupons = coupons,
                SavedPrice = savedMoney,  //coupon saved amt
                walletHeader = WalletHead,
                user = user,
                offered = Offered,
                ShipCharge = shipCharge,
                FilteredCoupon = unitofWork.Coupon.GetAll(c => !c.isReferral).ToList(),
                OriginalPrice = getCartList(UserId).Sum(item => item.products.ProductPrice * item.Count)


            };


            if (FromWishlist.HasValue & TempData["errorMessage"] == null)
            {
                var prod = unitofWork.Cart.Get(c => c.productId == Id && c.UserId == UserId);

                if (prod != null)
                {
                    var wishList = unitofWork.Wishlist.Get(u => u.UserId == UserId && u.productId == prod.productId);

                    if (wishList != null)
                    {
                        unitofWork.Wishlist.Delete(wishList);
                        unitofWork.Save();
                    }
                }
            }
            return orderViewModel;
        }
        private string getUserId()
        {
            return IdentityHelper.GetUserId(User);
        }
    }
}