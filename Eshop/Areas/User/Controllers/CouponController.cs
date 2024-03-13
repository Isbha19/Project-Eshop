using Eshop.Data.Data;
using Eshop.Data.Repository;
using Eshop.Model.Models;
using Eshop.Model.ViewModels;
using Eshop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stripe;
using System.Security.Claims;

namespace Eshop.Areas.User.Controllers
{
	[Area("User")]
	[Authorize]
	public class CouponController : Controller
	{
		private readonly IUnitofWork unitofWork;

		public CouponController(IUnitofWork unitofWork)
		{
			this.unitofWork = unitofWork;
		}
		[HttpPost]
		public IActionResult CouponApply(string couponCode)
		{
			HttpContext.Session.SetString("CouponCode", couponCode);

			if (ModelState.IsValid)
			{
				var userId = IdentityHelper.GetUserId(User);
				var user=unitofWork.User.Get(u=>u.Id== userId);
				var coupon = unitofWork.Coupon.Get(u => u.Code == couponCode);
				if (coupon == null)
				{
					TempData["errorMessage"] = "Coupon Code Not Valid";

					return RedirectToAction("Cart", "Cart");
				}
				if (coupon.Code==SD.RefferalFirstOrder && !user.isRefferalFlag)
				{
					TempData["errorMessage"] = "Invalid Coupon";
					return RedirectToAction("Cart", "Cart");
				}
				
				var discountPercent = coupon.Percentage;
				var cartItems = unitofWork.Cart.GetAll(u => u.UserId == userId, includeProperties: "products,products.productImages");
				double savedAmount = 0;
				

				foreach (var cartItem in cartItems)
				{
					
					
					if (cartItem.products.StockLeft > 0)
					{
						double dividedDiscount = discountPercent / cartItems.Count();
						var AmountToReduce= cartItem.TotalPrice * (dividedDiscount / 100);

						cartItem.CouponDiscountPrice = cartItem.TotalPrice - AmountToReduce;
						cartItem.CouponApplied = true;
						savedAmount += AmountToReduce;
						unitofWork.Cart.update(cartItem);
						unitofWork.Save();
					}
				}
				


                TempData["successMessage"] = "You Saved Rs." + savedAmount + " on your Order!!";
			}
			else
			{
				TempData["errorMessage"] = "Please add the code";
				
			}

			return RedirectToAction("Cart", "Cart");
		}
		[HttpPost]
		public IActionResult RemoveCoupon()
		{
			var userId = IdentityHelper.GetUserId(User);
			var cartItems = unitofWork.Cart.GetAll(u => u.UserId == userId, includeProperties: "products,products.productImages");

			foreach (var cartItem in cartItems)
			{
				if (cartItem.CouponApplied)
				{
					// Remove the discount applied to this cart item
					cartItem.CouponDiscountPrice =0;
					cartItem.CouponApplied = false;
					unitofWork.Cart.update(cartItem);
				}
			}

			unitofWork.Save();
			TempData["successMessage"] = "Coupon removed successfully.";
			return RedirectToAction("Cart", "Cart");
		}

	}
}
