using Eshop.Data.Repository;
using Eshop.Model.Models;
using Eshop.Model.ViewModels;
using Eshop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class WalletController : Controller
    {
        private readonly IUnitofWork unitofWork;


        public WalletController(IUnitofWork unitofWork)
        {

            this.unitofWork = unitofWork;
        }
        public IActionResult Index()
        {
            string userId = IdentityHelper.GetUserId(User);
            WalletHeader walletHead = unitofWork.WalletHeader.Get(u => u.UserId == userId,includeProperties: "wallets");
         


            return View(walletHead);
        }

        public IActionResult FilterWallet(DateTime startDate, DateTime endDate)
        {
            if (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
            {
                TempData["errorMessage"] = "Please select both dates";

                return RedirectToAction("Index");
            }

            if (startDate < endDate )
            {
                string userId = IdentityHelper.GetUserId(User);
				WalletHeader walletHead = unitofWork.WalletHeader.Get(u => u.UserId == userId, includeProperties: "wallets");

				walletHead.wallets= unitofWork.Wallet.GetAll(u=> u.TransactionDate>=startDate && u.TransactionDate<=endDate).ToList();
                
                return View("Index", walletHead);
            }
            else if(startDate>endDate) 
            {
                TempData["errorMessage"] = "Pick start date less than end date";
            }

            return RedirectToAction("Index");
        }
       
    }
}
