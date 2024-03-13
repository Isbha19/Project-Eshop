
using Eshop.Data.Migrations;
using Eshop.Data.Repository;
using Eshop.Model.Models;
using Eshop.Model.ViewModels;
using Eshop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static Eshop.Model.Models.CategoryOffer;
namespace Eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class CategoryOfferController : Controller
    {



        private readonly IUnitofWork unitofWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CategoryOfferController(IUnitofWork unitofWork, IWebHostEnvironment webHostEnvironment)
        {


            this.unitofWork = unitofWork;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult CategoryOffer()
        {

            List<CategoryOffer> CategoryOffer = unitofWork.CategoryOffer.GetAll(includeProperties: "offer,category").ToList();
           
       
           
            return View(CategoryOffer);
        }


        [HttpPost]
        public IActionResult Upsert(CategoryOffer? CategoryOffer, int? Id)
        {

            if (Id != null)
            {
                unitofWork.CategoryOffer.update(CategoryOffer);
                TempData["successMessage"] = "Category Offer Updated Successfully";
            }
            else
            {
                var offer = unitofWork.Offer.Get(u => u.OfferId == CategoryOffer.OfferId);
                var category = unitofWork.Category.Get(u => u.Id == CategoryOffer.CategoryId, includeProperties: "products");
                foreach (var product in category.products)
                {
                    if (offer != null && offer.IsActive && !product.isOffered)
                    {
                        product.isOffered = true;
                        product.OfferType = SD.CategoryOffer;
                        if (offer.offerType == Offer.OfferType.Percentage)
                        {
                            // Calculate the discounted price based on the percentage discount
                            double discountPercentage = offer.Discount;
							double productPrice = product.ProductPrice;
							double discountFraction = discountPercentage / 100; // Convert percentage to fraction
							double discountedAmount = productPrice * discountFraction;
							double CouponDiscountPrice = productPrice - discountedAmount;

                            // Update the product's discount price and offer name
                            product.OfferPrice = CouponDiscountPrice;
                            product.OfferName = offer.OfferName;
                            // Update the product's discount and save changes
                            unitofWork.Product.updateOffer(product);
                            unitofWork.Save();
                        }
                        else if (offer.offerType == Offer.OfferType.FixedAmount)
                        {
                            var productTotal = product.ProductPrice;
                            var disountedAmt = offer.Discount;
                            product.OfferPrice = productTotal - disountedAmt;
                            product.OfferName = offer.OfferName;
                            unitofWork.Product.updateOffer(product);
                            unitofWork.Save();

                        }
                    }
                    
                }
                var Catgry = unitofWork.Category.Get(u => u.Id == CategoryOffer.CategoryId);
                Catgry.IsDiscount = true;

                unitofWork.Category.update(Catgry);
                unitofWork.Save();
                unitofWork.CategoryOffer.Add(CategoryOffer);
                unitofWork.Save();

            }
            TempData["successMessage"] = "Category Offer Created Successfully";

            return RedirectToAction("CategoryOffer");
        }
        public IActionResult Upsert(int? Id)
        {
            IEnumerable<SelectListItem> CategoryList = unitofWork.Category.GetAll(u=>u.IsDiscount==false).Select(u =>
            new SelectListItem
            {
                Text = u.CategoryName,
                Value = u.Id.ToString()
            }
            );
            IEnumerable<SelectListItem> offerList = unitofWork.Offer.GetAll().Select(u =>
           new SelectListItem
           {
               Text = u.OfferName,
               Value = u.OfferId.ToString()
           }
           );

            CategoryOfferViewModel categoryOfferViewModel=new CategoryOfferViewModel()
            {
                categoryList= CategoryList,
                offerList= offerList,
                categoryOffer=new CategoryOffer() 
            };
           
            if (Id == null)
            {
                return View(categoryOfferViewModel);
            }
            else
            {
                CategoryOffer? categoryOffer = unitofWork.CategoryOffer.Get(c => c.Id == Id, includeProperties: "offer,category");
                categoryOfferViewModel.categoryOffer = categoryOffer;
                return View(categoryOfferViewModel);
            }
        }


   
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }
            CategoryOffer? categoryOffer = unitofWork.CategoryOffer.Get(c => c.Id == id, includeProperties: "offer,category");

            return View(categoryOffer);

        }
        [HttpPost, ActionName("Delete")]


        public IActionResult DeleteCategoryOffer(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var data = unitofWork.CategoryOffer.Get(c => c.Id == Id,includeProperties:"category,category.products");
            if (data != null)
            {
                data.category.IsDiscount = false;
                
                unitofWork.Category.update(data.category);
                unitofWork.Save();
               foreach(var product in data.category.products)
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





                
                var dataToDelete = unitofWork.CategoryOffer.Get(c => c.Id == Id);
                unitofWork.CategoryOffer.Delete(dataToDelete);
                unitofWork.Save();
                TempData["successMessage"] = "Category Offer Deleted Successfully";
                return RedirectToAction("CategoryOffer");


            }
            return View(data);

        }
      
    }
}
