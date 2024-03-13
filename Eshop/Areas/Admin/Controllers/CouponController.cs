
using Eshop.Data.Repository;
using Eshop.Model.Models;
using Eshop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class CouponController : Controller
    {



        private readonly IUnitofWork unitofWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CouponController(IUnitofWork unitofWork, IWebHostEnvironment webHostEnvironment)
        {


            this.unitofWork = unitofWork;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Coupon()
        {
            List<Coupon> Coupon = unitofWork.Coupon.GetAll().ToList();
            HandleExpiration();
            return View(Coupon);
        }


        [HttpPost]
        public IActionResult Upsert(Coupon? coupon, int? Id)
        {


            if (ModelState.IsValid)
            {
                if (coupon.ExpireDate < DateOnly.FromDateTime(DateTime.Today))
                {
                    ModelState.AddModelError(nameof(coupon.ExpireDate), "Expiration date must be in the future.");
                    return View(coupon);
                }
               
                if (Id != 0)
                {
                    unitofWork.Coupon.update(coupon);
                    TempData["successMessage"] = "Coupon " + SD.Updated;
                }
                else
                {
                    unitofWork.Coupon.Add(coupon);
                    TempData["successMessage"] = "Coupon " + SD.Created;
                }
                unitofWork.Save();
                return RedirectToAction("Coupon");
            }
            else
            {
                return View(coupon);
            }

            
        }
        public IActionResult Upsert(int? Id)
        {
            if (Id == null)
            {
                return View(new Coupon());
            }
            Coupon? Coupon = unitofWork.Coupon.Get(c => c.Id == Id);

            return View(Coupon);
        }



        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Coupon? Coupon = unitofWork.Coupon.Get(c => c.Id == id);
            unitofWork.Coupon.Delete(Coupon,TempData);
            unitofWork.Save();
            TempData["successMessage"] = "Coupon Deleted Successfully";
            List<Coupon> Coupons = unitofWork.Coupon.GetAll().ToList();
          
            return View("Coupon",Coupons);

        }

		public void HandleExpiration()
		{
			var coupons = unitofWork.Coupon.GetAll();

			foreach (var coupon in coupons)
			{
				if (coupon.ExpireDate < DateOnly.FromDateTime(DateTime.Today))
				{
					coupon.isActive = false;
					unitofWork.Coupon.update(coupon);
				}
			}

			unitofWork.Save();
		}

	}

}
