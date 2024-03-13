using Eshop.Data.Data;
using Eshop.Data.Repository;
using Eshop.Models;
using Eshop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class CategoryController : Controller
	{
        
	
        
        private readonly IUnitofWork unitofWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CategoryController(IUnitofWork unitofWork,IWebHostEnvironment webHostEnvironment)
        {
			
            
            this.unitofWork = unitofWork;
            this.webHostEnvironment = webHostEnvironment;
        }
       
        public IActionResult Category()
		{
            List<Category> categories = unitofWork.Category.GetAll().ToList();
			return View(categories);
		}
       
    
        [HttpPost]
        public IActionResult Upsert(Category? category, IFormFile? file,int? Id)
        {
            if (ModelState.IsValid)
            {
                string wwwrootPath = webHostEnvironment.WebRootPath;

                // Check if a new file is provided
                if (file != null)
                {
                    string productPath = Path.Combine(wwwrootPath, @"images\category");
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    // Check and delete the old image if it exists
                    if (!string.IsNullOrEmpty(category.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwrootPath, category.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Save the new image
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    // Update the ImageUrl
                    category.ImageUrl = @"\images\category\" + fileName;
                }

                if (Id != 0)
                {
                    unitofWork.Category.update(category);
                    TempData["successMessage"] = "Category Updated Successfully";
                    unitofWork.Save();
                }
                else
                {
                    unitofWork.Category.Add(category);
                    TempData["successMessage"] = "Category Created Successfully";
                    unitofWork.Save();
                }
              
                return RedirectToAction("Category");
            }

            return View(category);
        }
        public IActionResult Upsert(int? Id)
        {
            if (Id == null)
            {
                return View(new Category());
            }
            Category? category = unitofWork.Category.Get(c => c.Id == Id);

            return View(category);
        }
    

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category? category = unitofWork.Category.Get(c => c.Id == id);

            return View(category);
           
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category? category = unitofWork.Category.Get(c => c.Id == id);

            return View(category);

        }
        [HttpPost, ActionName("Delete")]
        
        
        public IActionResult DeleteCategory(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var data = unitofWork.Category.Get(c => c.Id == Id);
            if(data!=null)
            {
               unitofWork.Category.Delete(data);
                unitofWork.Save();
                TempData["successMessage"] = "Category Deleted Successfully";
                return RedirectToAction("Category");


            }
            return View(data);


        }
    }
}
