//using Eshop.Data.Migrations;
using Eshop.Data.Repository;
//using Eshop.Migrations;
using Eshop.Model.Models;
using Eshop.Model.ViewModels;
using Eshop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;


namespace Eshop.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUnitofWork unitofWork;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        public UserController(IUnitofWork unitofWork, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.unitofWork = unitofWork;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
		public IActionResult UserProfile()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var UserId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = unitofWork.User.Get(u => u.Id == UserId);

            return View(user);
        }
        public IActionResult EditProfile()
        {
            List<SelectListItem> genderList= new List<SelectListItem>
            {
               new SelectListItem
               {
                   Text="Male",
                   Value="Male"
               },
               new SelectListItem
               {
                   Text="Female",
                   Value="Female"
               }
            };
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var UserId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = unitofWork.User.Get(u => u.Id == UserId);

            UserViewModel userView = new UserViewModel
            {
                appUser=user,
                GenderList= genderList,
            };
            return View(userView);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(UserViewModel userDet)
        {
            var user = await userManager.FindByIdAsync(userDet.appUser.Id);
            if (user == null)
            {
                return NotFound();
            }
            // Check if the new email is unique
            var existingUser = await userManager.FindByEmailAsync(userDet.appUser.Email);
            if (existingUser != null && existingUser.Id != userDet.appUser.Id)
            {
                // The new email is already associated with another user
                ModelState.AddModelError("Email", "The email address is already in use.");
                return View(userDet.appUser);
            }

            user.PhoneNumber = userDet.appUser.PhoneNumber;
            user.Name = userDet.appUser.Name;
            user.Email = userDet.appUser.Email;
            user.Gender = userDet.appUser.Gender;
            user.AlternatePhoneNum= userDet.appUser.AlternatePhoneNum;
           user.Location = userDet.appUser.Location;
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                TempData["successMessage"] = "User Updated Successfully";
                return RedirectToAction("UserProfile", userDet.appUser);
            }
            return View(userDet);
        }
		

	}
}
