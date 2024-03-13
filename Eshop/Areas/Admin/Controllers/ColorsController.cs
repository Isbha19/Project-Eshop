
using Eshop.Data.Repository;
using Eshop.Model.Models;
using Eshop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class ColorsController : Controller
    {



        private readonly IUnitofWork unitofWork;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ColorsController(IUnitofWork unitofWork, IWebHostEnvironment webHostEnvironment)
        {


            this.unitofWork = unitofWork;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Colors()
        {
            List<Colors> colors = unitofWork.Colors.GetAll().ToList();
           
            return View(colors);
        }


        [HttpPost]
        public IActionResult Upsert(Colors? Colors, int? Id)
        {
            

                if (Id != 0)
                {
                    unitofWork.Colors.update(Colors);
                    TempData["successMessage"] = "Color Updated Successfully";
                }
                else
                {
                    unitofWork.Colors.Add(Colors);
                    TempData["successMessage"] = "Color Created Successfully";
                }
                unitofWork.Save();
                return RedirectToAction("Colors");

            return View(Colors);
        }
        public IActionResult Upsert(int? Id)
        {
            if (Id == null)
            {
                return View(new Colors());
            }
            Colors? Colors = unitofWork.Colors.Get(c => c.Id == Id);

            return View(Colors);
        }


   
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Colors? Colors = unitofWork.Colors.Get(c => c.Id == id);

            return View(Colors);

        }
        [HttpPost, ActionName("Delete")]


        public IActionResult DeleteColors(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var data = unitofWork.Colors.Get(c => c.Id == Id);
            if (data != null)
            {
                unitofWork.Colors.Delete(data);
                unitofWork.Save();
                TempData["successMessage"] = "Color Deleted Successfully";
                return RedirectToAction("Colors");


            }
            return View(data);


        }
    }
}
