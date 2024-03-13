using Eshop.Data.Repository;
using Eshop.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Eshop.Data.Data;

namespace Eshop.Areas.User.Controllers
{
	[Area("User")]
	[Authorize]
	public class AddressController : Controller
	{
		private readonly IUnitofWork unitofWork;
		private readonly ApplicationDbContext context;

		public AddressController(IUnitofWork unitofWork,ApplicationDbContext context)
        {
			this.unitofWork = unitofWork;
			this.context = context;
			
		}
        public IActionResult Index()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var UserId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier).Value;
			var addresses = unitofWork.Address.GetAll(c=>c.UserId==UserId);
			var orderedAddresses = addresses.OrderByDescending(a => a.IsDefault).ToList();
			

			return View(orderedAddresses);
		}
		public IActionResult Create()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var UserId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier).Value;



			ShippingAdress shippingAdress = new ShippingAdress()
			{
				UserId = UserId,
				IsDefault = false
			};
			return View(shippingAdress);
		}
		[HttpPost]
		public IActionResult Create(ShippingAdress? addressValue)
		{
			try
			{
				if (ModelState.IsValid)
				{
					var claimsIdentityitem = (ClaimsIdentity)User.Identity;
					var UserIditem = claimsIdentityitem?.FindFirst(ClaimTypes.NameIdentifier).Value;
					var address = unitofWork.Address.GetAll(u => u.UserId == UserIditem);
					if(address.Count()==0)
					{
						addressValue.IsDefault = true;
					}
					if (addressValue.IsDefault == true)
					{
						
						var addressesitem = unitofWork.Address.GetAll(c => c.UserId == UserIditem && c.Id != addressValue.Id);

						if (addressesitem.Count() >= 1)
						{
							foreach (var item in addressesitem)
							{
								item.IsDefault = false;
								unitofWork.Address.update(item);
							}
							unitofWork.Save();
						}
						

					}

					unitofWork.Address.Add(addressValue);
					unitofWork.Save();
					TempData["successMessage"] = "Address Saved Successfully";

					var claimsIdentity = (ClaimsIdentity)User.Identity;
					var UserId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier).Value;
					var addresses = unitofWork.Address.GetAll(c => c.UserId == UserId);
					var orderedAddresses = addresses.OrderByDescending(a => a.IsDefault).ToList();

					return RedirectToAction("Index", orderedAddresses);

				}
			}
			catch (Exception ex)
			{
				// Log or handle the exception as needed
				TempData["errorMessage"] = "Error saving the address: " + ex.Message;
			}
			return View(addressValue);
		}
		public IActionResult Delete(int? Id)
		{
			try
			{
				if (Id != null)
				{
					var dataToDelete = unitofWork.Address.Get(c => c.Id == Id);



					//List<ShippingAdress> addresses = unitofWork.Address.GetAll(c => c.UserId == UserId).ToList();
					//List<ShippingAdress> orderedAddresses = addresses.OrderByDescending(a => a.IsDefault).ToList();
					//context.Entry(dataToDelete).State = EntityState.Detached;
					var claimsIdentity = (ClaimsIdentity)User.Identity;
					var UserId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier).Value;

					List<ShippingAdress> orderedAddresses = unitofWork.Address.GetAll(c => c.UserId == UserId && c.Id != dataToDelete.Id).ToList();


					if (dataToDelete.IsDefault && orderedAddresses.Count>0)
					{

						orderedAddresses[0].IsDefault = true;


					}


					unitofWork.Address.Delete(dataToDelete);
					unitofWork.Save();
				


					TempData["successMessage"] = "Address Deleted Successfully";
				}
			} catch (Exception ex)
			{
				// Log or handle the exception as needed
				TempData["errorMessage"] = "Error deleting the address: " + ex.Message;
			}
			return RedirectToAction(nameof(Index));
		}

		public IActionResult Edit(int? Id)
		{
			var data=unitofWork.Address.Get(c=>c.Id== Id);

			return View(data);
		}
		[HttpPost]
		public IActionResult Edit(ShippingAdress addressItem)
		{
			try
			{
				unitofWork.Address.update(addressItem);
				unitofWork.Save();
				var claimsIdentity = (ClaimsIdentity)User.Identity;
				var UserId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier).Value;

				if (!addressItem.IsDefault)
				{
					var addresses = unitofWork.Address.GetAll(c => c.UserId == UserId && c.Id != addressItem.Id);
					var orderedAddresses = addresses.OrderByDescending(a => a.IsDefault).ToList();
					orderedAddresses[0].IsDefault = true;
					unitofWork.Address.update(orderedAddresses[0]);
					unitofWork.Save();
				}

				if (addressItem.IsDefault)
				{
					var addresses = unitofWork.Address.GetAll(c => c.UserId == UserId && c.Id != addressItem.Id);

					foreach (var item in addresses)
					{
						item.IsDefault = false;
						unitofWork.Address.update(item);

					}
					unitofWork.Save();

				}


			}
			catch (Exception ex)
			{
				// Log or handle the exception as needed
				TempData["errorMessage"] = "Error updating the address: " + ex.Message;
			}
			return RedirectToAction(nameof(Index));
		}
		
			public IActionResult AddressChange()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var UserId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier).Value;
			var addresses = unitofWork.Address.GetAll(c => c.UserId == UserId);
			var orderedAddresses = addresses.OrderByDescending(a => a.IsDefault).ToList();

			return View(orderedAddresses);
		}
	}
}
