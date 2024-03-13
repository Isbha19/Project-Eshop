
using Eshop.Data.Data;
using Eshop.Data.Migrations;
using Eshop.Data.Repository;
using Eshop.Model.Models;
using Eshop.Model.ViewModels;
using Eshop.Utility;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static Eshop.Model.Models.ProductOffer;
namespace Eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class ProductOfferController : Controller
    {



        private readonly IUnitofWork unitofWork;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ApplicationDbContext dbContext;

        public ProductOfferController(IUnitofWork unitofWork, IWebHostEnvironment webHostEnvironment,ApplicationDbContext dbContext)
        {


            this.unitofWork = unitofWork;
            this.webHostEnvironment = webHostEnvironment;
            this.dbContext = dbContext;
        }

        public IActionResult ProductOffer()
        {

            List<ProductOffer> ProductOffer = unitofWork.ProductOffer.GetAll(includeProperties: "offer,product").ToList();
           
       
           
            return View(ProductOffer);
        }


        [HttpPost]
        public IActionResult Upsert(ProductOffer? ProductOffer, int? Id)
        {
            
            if (Id != null)
                {

                    unitofWork.ProductOffer.update(ProductOffer);
                    TempData["successMessage"] = "Product Offer Updated Successfully";
                }
                else
                {
                var offer = unitofWork.Offer.Get(u => u.OfferId == ProductOffer.OfferId);
                var product = unitofWork.Product.Get(u => u.Id == ProductOffer.ProductId);
                if (offer != null && offer.IsActive && !product.isOffered) {
                
                product.isOffered = true;
                    product.OfferType = SD.ProductOffer;
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
                    else if(offer.offerType == Offer.OfferType.FixedAmount)
                    {
                        var productTotal = product.ProductPrice;
                        var disountedAmt = offer.Discount;
                        product.OfferPrice = productTotal - disountedAmt;
                        product.OfferName = offer.OfferName;
                        unitofWork.Product.updateOffer(product);
                        unitofWork.Save();

                    }


                    unitofWork.ProductOffer.Add(ProductOffer);
                     unitofWork.Save();
                    TempData["successMessage"] = "Product Offer Created Successfully";

                }
                else
                {
                    TempData["successMessage"] = "Product Offer Creation Not successfull";
                }
                }
               
                return RedirectToAction("ProductOffer");

            return View(ProductOffer);
        }
        public IActionResult Upsert(int? Id)
        {
         
            IEnumerable<SelectListItem> ProductsList = unitofWork.Product.GetAll(u=>u.isOffered==false).Select(u =>
            new SelectListItem
            {
                Text = u.ProductName,
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

            ProductOfferViewModel productOfferViewModel = new ProductOfferViewModel() {
                productList = ProductsList,
                offerList = offerList,
                productOffer = new ProductOffer()


            };
            if (Id == null)
            {
                return View(productOfferViewModel);
            }
            else
            {
                ProductOffer? ProductOffer = unitofWork.ProductOffer.Get(c => c.Id == Id, includeProperties: "offer,product");
                productOfferViewModel.productOffer = ProductOffer;
                return View(productOfferViewModel);
            }
        }


   
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }
            ProductOffer? ProductOffer = unitofWork.ProductOffer.Get(c => c.Id == id, includeProperties: "offer,product");

            return View(ProductOffer);

        }
        [HttpPost, ActionName("Delete")]


        public IActionResult DeleteProductOffer(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var data = unitofWork.ProductOffer.Get(c => c.Id == Id,includeProperties: "offer,product");
            if (data != null)
            {
                data.product.isOffered = false;
                data.product.OfferPrice = 0;
                data.product.OfferName = "";
                data.product.OfferType = "";
                unitofWork.Product.updateOffer(data.product);
                unitofWork.Save();

                var dataToDelete = unitofWork.ProductOffer.Get(c => c.Id == Id);


                unitofWork.ProductOffer.Delete(dataToDelete);
                unitofWork.Save();
                TempData["successMessage"] = "Product offer Deleted Successfully";
                return RedirectToAction("ProductOffer");


            }
            return View(data);

        }

      
    }
}
