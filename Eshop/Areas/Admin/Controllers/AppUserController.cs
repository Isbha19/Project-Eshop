using Eshop.Model.Models;
using Eshop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;

namespace Eshop.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
  
    public class AppUserController : Controller
	{
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
		private readonly IEmailSender _emailSender;
      
        public AppUserController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
			this._emailSender = emailSender;
		}
        public IActionResult AppUser()
		{
            var users=userManager.Users;
			return View(users);
		}
        public IActionResult Create() => View();
       

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUser appUser)
        {
            if (ModelState.IsValid)
            {
                // You can customize this based on your registration form
                var user = new ApplicationUser
                {
                    UserName= appUser.UserName,
                    Name = appUser.Name,
                    Email = appUser.Email,
                    PhoneNumber = appUser.PhoneNumber,
                    // Add other properties as needed
                };

                var result = await userManager.CreateAsync(user,appUser.PasswordHash);

				if (result.Succeeded)
				{
					// Check if the user is created by the admin

					if (User.IsInRole(SD.Role_Admin))
					{
                        // Generate email confirmation token
                        var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = user.Id, code = code },
                            protocol: Request.Scheme);


                        // Send email confirmation email
                        await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
							$"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
					}
					// You may choose to sign in the user after registration
					// await signInManager.SignInAsync(user, isPersistent: false);
					TempData["successMessage"] = "User Created Successfully";
                    return RedirectToAction("AppUser");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(appUser);
        }
        public async Task<IActionResult> Edit(string? Id)
        {
            var user= await userManager.FindByIdAsync(Id);
            if(user == null)
            {
                return NotFound();
            }
           
           
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ApplicationUser appUser)
        {
            var user = await userManager.FindByIdAsync(appUser.Id);
            if (user == null)
            {
                return NotFound();
            }
            // Check if the new email is unique
            var existingUser = await userManager.FindByEmailAsync(appUser.Email);
            if (existingUser != null && existingUser.Id != appUser.Id)
            {
                // The new email is already associated with another user
                ModelState.AddModelError("Email", "The email address is already in use.");
                return View(appUser);
            }

            user.PhoneNumber = appUser.PhoneNumber;
               user.Name = appUser.Name;
                user.Email = appUser.Email;
             var result= await userManager.UpdateAsync(user);
            if(result.Succeeded)
            {
                TempData["successMessage"] = "User Updated Successfully";
                return RedirectToAction("AppUser");
            }
            return View(appUser);
        }
        public async Task<IActionResult> Delete(String Id)
        {
            var user=await userManager.FindByIdAsync(Id);

            
            return View(user);
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteUser(String Id)
        {
            var user = await userManager.FindByIdAsync(Id);

            try
            {
                if (user == null)
                {
                    return NotFound();
                }

                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    TempData["successMessage"] = "User Deleted Successfully";
                    return RedirectToAction("AppUser");
                }

                return View("AppUser");
            }catch (Exception ex)
            {
                TempData["errorMessage"]=ex.Message;
                return View(user);

            }
        }

        public async Task<IActionResult> Block(String Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var user = await userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return NotFound();
            }

            user.LockoutEnd= DateTime.Now.AddDays(10);
            await userManager.UpdateAsync(user);
            TempData["successMessage"] = "User Blocked Successfully";


            return RedirectToAction("AppUser");
            
        }
        public async Task<IActionResult> UnBlock(String Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var user = await userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return NotFound();
            }

            user.LockoutEnd = null; // Unlock the user
            await userManager.UpdateAsync(user);
            TempData["successMessage"] = "User Unblocked Successfully";


            return RedirectToAction("AppUser");

        }

    }
}
