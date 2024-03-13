using Eshop.Data.Data;
using Eshop.Model.Models;
using Eshop.Data.Repository;
using Eshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Eshop.Model.ViewModels;

using Humanizer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Eshop.Utility;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
namespace Eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class ProductController : Controller
	{
        
	
        
        private readonly IUnitofWork unitofWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(IUnitofWork unitofWork,IWebHostEnvironment webHostEnvironment)
        {
			
            
            this.unitofWork = unitofWork;
            this.webHostEnvironment = webHostEnvironment;
        }
       
        public IActionResult Product()
		{
            
            List<Product> products = unitofWork.Product.GetAll(includeProperties: "category,colors,productImages").ToList();
            
			return View(products);
		}

        public IActionResult Upsert(int? Id)
        {
            IEnumerable<SelectListItem> CategoryList = unitofWork.Category.GetAll().Select(u =>
            new SelectListItem
            {
                Text = u.CategoryName,
                Value = u.Id.ToString()
            }
            );
            IEnumerable<SelectListItem> ColorsList = unitofWork.Colors.GetAll().Select(u =>
           new SelectListItem
           {
               Text = u.Color,
               Value = u.Id.ToString()
           }
           );
          

            ProductViewModel productViewModel = new ProductViewModel
            {
                CategoryList = CategoryList,
                product = new Product(),
                Colors = ColorsList,
                
                
            };
            if (Id == null)
            {
                return View(productViewModel);
            }
            else
            {
                Product? Product = unitofWork.Product.Get(c => c.Id == Id, includeProperties: "productImages");
                productViewModel.product = Product;
                return View(productViewModel);
            }


        }
        [HttpPost]
        public IActionResult Upsert(ProductViewModel? productViewModel, List<IFormFile>? files)
        {
            if (ModelState.IsValid)
            {
                productViewModel.product.CreatedAt= DateTime.Now;
                if (productViewModel.product.Id == 0)
                {
                    unitofWork.Product.Add(productViewModel.product);
                    productViewModel.product.StockLeft = productViewModel.product.Quantity;
                    TempData["successMessage"] = "Product Created Successfully";
                }
                else
                {
                    if (productViewModel.product.StockLeft > productViewModel.product.Quantity)
                    {
                        TempData["errorMessage"] = "Stock left can't be greater than the Quantity of product";
                        productViewModel.CategoryList = unitofWork.Category.GetAll().Select(u =>

                new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.Id.ToString(),
                });
                        productViewModel.Colors = unitofWork.Colors.GetAll().Select(u =>

                       new SelectListItem
                       {
                           Text = u.Color,
                           Value = u.Id.ToString(),
                       });
                        return View(productViewModel);

                    }
                    
                    unitofWork.Product.update(productViewModel.product);
                    TempData["successMessage"] = "Product Updated Successfully";

                }
                unitofWork.Save();
               
                




                string wwwRootPath = webHostEnvironment.WebRootPath;

                if (files != null)
                {

                    foreach (var file in files)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                        string productPath = @"images\Product-" + productViewModel.product.Id;
                        string finalPath = Path.Combine(wwwRootPath, productPath);

                        if (!Directory.Exists(finalPath))
                        {
                            Directory.CreateDirectory(finalPath);
                        }
                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        ProductImage productImage = new ProductImage
                        {
                            ImageUrl = @"\" + productPath + @"\" + fileName,
                            ProductId = productViewModel.product.Id,

                        };
                        productViewModel.product.productImages.Add(productImage);

                    }
                   

                    unitofWork.Product.update(productViewModel.product);
                    unitofWork.Save();
                }



                return RedirectToAction("Product");
            }
            else
            {
                productViewModel.CategoryList = unitofWork.Category.GetAll().Select(u =>

                new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.Id.ToString(),
                });
                productViewModel.Colors = unitofWork.Colors.GetAll().Select(u =>

               new SelectListItem
               {
                   Text = u.Color,
                   Value = u.Id.ToString(),
               });
            }

            return View(productViewModel);
        }
      
    

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product? Product = unitofWork.Product.Get(c => c.Id == id,includeProperties: "category,productImages,colors");

            return View(Product);
           
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Product? Product = unitofWork.Product.Get(c => c.Id == id,includeProperties: "category");

            return View(Product);

        }
        [HttpPost, ActionName("Delete")]
        
        
        public IActionResult DeleteProduct(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var data = unitofWork.Product.Get(c => c.Id == Id);
            if(data!=null)
            {
               unitofWork.Product.Delete(data);
                unitofWork.Save();
                TempData["successMessage"] = "Product Deleted Successfully";
                return RedirectToAction("Product");


            }
            return View(data);


        }
        public IActionResult DeleteImage(int? Id)
        {
            var ImageToBeDeleted=unitofWork.ProductImage.Get(c=> c.Id == Id);
            int prodId = ImageToBeDeleted.ProductId;
            if (ImageToBeDeleted!=null)
            {
               
                if (!string.IsNullOrEmpty(ImageToBeDeleted.ImageUrl))
                {
                    var oldImagePath = Path.Combine(webHostEnvironment.WebRootPath, ImageToBeDeleted.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                unitofWork.ProductImage.Delete(ImageToBeDeleted);
                unitofWork.Save();
                TempData["successMessage"] = "Image deleted successfully";
            }
            return RedirectToAction(nameof(Upsert), new { Id = prodId });
            //return Content("<script>window.location.href = '" + Url.Action("Upsert", new { Id = prodId }) + "';</script>", "text/html");

        }

    }
}
