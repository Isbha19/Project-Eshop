using Eshop.Data.Repository;
using Eshop.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Eshop.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class WishlistController : Controller
	{
		private readonly IUnitofWork unitofWork;

		public WishlistController(IUnitofWork unitofWork)
        {
			this.unitofWork = unitofWork;
		}
		public IActionResult Index()
		{
			string userId=getUserId();
			List<Wishlist> wishlists = unitofWork.Wishlist.GetAll(u=>u.UserId==userId,includeProperties: "product,product.productImages").ToList();

			return View(wishlists);
		}
		public IActionResult Add(int? Id)
		{
			if (Id != null)
			{
                var product = unitofWork.Product.Get(u => u.Id == Id);
				var wishlisted=unitofWork.Wishlist.Get(u=>u.productId== Id && u.UserId==getUserId());

				if (product != null && wishlisted==null)
				{
                    Wishlist wishlist = new Wishlist()
                    {
                        productId = product.Id,
                        UserId = getUserId(),
                    };
                    unitofWork.Wishlist.Add(wishlist);
					unitofWork.Save();

					TempData["successMessage"] = "Product Wishlisted";
                }
				else
				{
                    TempData["errorMessage"] = "Product already Wishlisted";

                }

            }
            string userId = getUserId();
            List<Wishlist> wishlists = unitofWork.Wishlist.GetAll(u => u.UserId == userId, includeProperties: "product,product.productImages").ToList();

            return View("Index", wishlists);
		}
        public IActionResult Delete(int? Id)
        {
            var wishlistToDelete=unitofWork.Wishlist.Get(u=>u.Id==Id);
			if (wishlistToDelete != null)
			{
				unitofWork.Wishlist.Delete(wishlistToDelete);
				unitofWork.Save();
			}
            string userId = getUserId();
            List<Wishlist> wishlists = unitofWork.Wishlist.GetAll(u => u.UserId == userId, includeProperties: "product,product.productImages").ToList();

            return View("Index", wishlists);
           
        }
        public string getUserId()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var UserId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier).Value;
			return UserId;
		}
	}
}
