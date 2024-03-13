using Eshop.Data.Repository;
//using Eshop.Migrations;
using Eshop.Model.Models;
using Eshop.Models;
using Eshop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Eshop.Areas.User.Controllers
{
    [Area("User")]

    public class HomeController : Controller
    {
        private readonly IUnitofWork unitofWork;
		private Wishlist wishlisted;

		public HomeController(IUnitofWork unitofWork)
        {
            this.unitofWork = unitofWork;
        }
        public IActionResult Index()
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
			List<Category> categories=unitofWork.Category.GetAll().ToList();
            return View(categories);
        }
        
		
		public IActionResult Products()
        {
            return View();

        }
        public IActionResult Register()
        {
            return View();
        }
		public IActionResult Login()
		{
			return View();
		}
		public IActionResult About()
		{
			return View();
		}
		public IActionResult _CategoryList()
		{
			var category=unitofWork.Category.GetAll();
			return PartialView("_CategoryList",category);
		}



		public IActionResult Contact()
		{
			return View();
		}

		public IActionResult Single(int? Id)
		{
			string userId=getUserId();
			var product = unitofWork.Product.Get(u => u.Id == Id, includeProperties: "productImages,colors");


			
			if (product!=null)
			{
				if (!string.IsNullOrEmpty(userId))
				{
					wishlisted = unitofWork.Wishlist.Get(u => u.productId == Id && u.UserId == userId);
					if (wishlisted != null)
					{
						product.isWishlisted = true;
					}
					else
					{
						product.isWishlisted = false;
					}

				}
				else { 
					product.isWishlisted = false;
				}
				
                product.ViewsCount=product.ViewsCount+1;
				unitofWork.Product.updateViewsCountandWishlist(product);
				unitofWork.Save();
			}
			return View(product);
		}
        public IActionResult CategoryProducts(string? sort,int? Id,string? searchString)
      {
			var category = unitofWork.Category.Get(u => u.Id == Id);
			if(category!=null)
			{
				HttpContext.Session.SetString("CategoryName", category.CategoryName);
			}
			
			var products=unitofWork.Product.GetAll(u=>u.CategoryId==Id, includeProperties: "productImages");
           
            DateTime LastWeek = DateTime.Today.AddDays(-7);
			switch (sort)
			{
				
				case "price_Asc":
					products = products.OrderBy(p => p.ProductPrice);
					break;
				case "price_desc":
					products = products.OrderByDescending(p => p.ProductPrice);
					break;
                case "aToZ":
                    products = products.OrderBy(p => p.ProductName);
                    break;
				case "NewArrivals":
                    products = products.Where(p => p.CreatedAt>=LastWeek);
                    break;
				case "popularity":
                    products = products.OrderByDescending(p => p.ViewsCount + p.OrderCount);
                    break;

                case "Featured":
                    products = products.Where(p => p.isFeatured==true);
                    break;
                case "AvgRating":
					products = products.OrderBy(p => p.Rating);
					break;
				case "zToA":
					products = products.OrderByDescending(p => p.ProductName);
					break;
				default:
					products = products.OrderBy(p => p.Id);
					break;


			}
			if (!string.IsNullOrEmpty(searchString))
			{
				products = products.Where(p => p.ProductName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0);


			}

			return View(products.ToList());
        }
        public string getUserId()
        {
			if (User.Identity != null && User.Identity.IsAuthenticated)
			{
				var claimsIdentity = (ClaimsIdentity)User.Identity;
				var UserId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier).Value;
				return UserId;

			}
			return null;
		}
    }
}
