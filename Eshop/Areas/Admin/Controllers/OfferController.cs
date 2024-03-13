
using Eshop.Data.Migrations;
using Eshop.Data.Repository;
using Eshop.Model.Models;
using Eshop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Eshop.Model.Models.Offer;
namespace Eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class OfferController : Controller
    {



        private readonly IUnitofWork unitofWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public OfferController(IUnitofWork unitofWork, IWebHostEnvironment webHostEnvironment)
        {


            this.unitofWork = unitofWork;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Offer()
        {

            List<Offer> Offer = unitofWork.Offer.GetAll().ToList();
           
       
           
            return View(Offer);
        }


        [HttpPost]
        public IActionResult Upsert(Offer? Offer, int? Id)
        {

            ViewBag.OfferTypes = GetOfferTypeSelectList();
            if (Offer.EndDate >= DateTime.Now)
                    {
                        Offer.IsActive = true;
                    }
                    else
                    {
                        Offer.IsActive = false;
                    }
                    if (Offer.StartDate > Offer.EndDate)
                    {
                TempData["errorMessage"] = "End date can't be less than Start date";
                        return View(Offer);
                    }
                    else if (Offer.StartDate <= DateTime.Now)
                    {
                TempData["errorMessage"] = "Start date can't be less than Today";
                        return View(Offer);
                    }
                if (Offer.offerType == Offer.OfferType.Percentage && Offer.Discount>100)
                {
                TempData["errorMessage"] = "Discount cannot be greater than 100% for percentage offers.";
                    return View(Offer);
                }

                    if (Id != null)
                    {
                        unitofWork.Offer.update(Offer);
                        TempData["successMessage"] = "Offer Updated Successfully";
                    }
                    else
                    {


                        unitofWork.Offer.Add(Offer);
                        TempData["successMessage"] = "Offer Created Successfully";
                    }
                    unitofWork.Save();
                    return RedirectToAction("Offer");


               

        }
        public IActionResult Upsert(int? Id)
        {
            ViewBag.OfferTypes = GetOfferTypeSelectList();
            if (Id == null)
            {
                return View(new Offer());
            }
            Offer? Offer = unitofWork.Offer.Get(c => c.OfferId == Id);
           
            Offer.StartDate = Offer.StartDate.Date;
            Offer.EndDate = Offer.EndDate.Date;

            return View(Offer);
        }


   
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }
            Offer? Offer = unitofWork.Offer.Get(c => c.OfferId == id);

            return View(Offer);

        }
        [HttpPost, ActionName("Delete")]


        public IActionResult DeleteOffer(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var data = unitofWork.Offer.Get(c => c.OfferId == Id);
            var productWithOffers=unitofWork.ProductOffer.GetAll(u=>u.OfferId== Id,includeProperties: "product");
            if (productWithOffers != null)
            {
                foreach (var item in productWithOffers)
                {
                    item.product.isOffered = false;
                    item.product.OfferPrice = 0;
                    item.product.OfferName = "";
                    item.product.OfferType = "";
                    unitofWork.Product.updateOffer(item.product);
                }
                unitofWork.Save();
            }
            var categoryWithOffers=unitofWork.CategoryOffer.GetAll(u=>u.OfferId== Id,includeProperties: "category,category.products",TempData);
            if(categoryWithOffers != null)
            {
                foreach (var item in categoryWithOffers)
                {
                    item.category.IsDiscount=false;
                    unitofWork.Category.update(item.category);
                    unitofWork.Save();
                    foreach (var product in item.category.products)
                    {
                        if (product.OfferType != SD.ProductOffer)
                        {
                            product.isOffered = false;
                            product.OfferPrice = 0;
                            product.OfferName = "";
                            product.OfferType = "";
                            unitofWork.Product.updateOffer(product);
                            unitofWork.Save();
                        }
                    }
                    
                }
            }

            if (data != null)
            {
                unitofWork.Offer.Delete(data);
                unitofWork.Save();
                TempData["successMessage"] = "Offer Deleted Successfully";
                return RedirectToAction("Offer");


            }

            return View(data);

        }
        private List<SelectListItem> GetOfferTypeSelectList()
        {
            var offerTypes = new List<SelectListItem>
    {
        new SelectListItem { Value = OfferType.Percentage.ToString(), Text = "Percentage" },
        new SelectListItem { Value = OfferType.FixedAmount.ToString(), Text = "Fixed Amount" }
    };
            return offerTypes;
        }
    }
}
