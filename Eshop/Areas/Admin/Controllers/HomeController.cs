using Eshop.Data.Data;
using Eshop.Data.Repository;
using Eshop.Model.Models;
using Eshop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Eshop.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]

	public class HomeController : Controller
	{
		private readonly ApplicationDbContext context;

        public IUnitofWork UnitofWork { get; }
		public HomeController(IUnitofWork unitofWork, ApplicationDbContext context)
		{
			UnitofWork = unitofWork;
			this.context = context;
        }



		public IActionResult Index(DateTime? startDate, DateTime? endDate)
		{
			var users = UnitofWork.User.GetAll();
			var UserCount = users.Count();
			var orderDetail = UnitofWork.OrderDetails.GetAll();

			double OverAllSalesCount = 0;
			double OverAllOrderAmount = 0;
			double OverAllDiscount = 0;

			foreach (var order in orderDetail)
			{
				if (order.ProductStatus == SD.statusDelivered)
				{
					OverAllSalesCount += order.Count;
					OverAllOrderAmount += order.Price;
					OverAllDiscount += order.discountSavedPrice;
				}


			}
			ViewBag.TotalSalesCount = OverAllSalesCount;
			ViewBag.OverAllOrderAmount = OverAllOrderAmount;
			ViewBag.UserCount = UserCount;
			ViewBag.OverAllDiscount = OverAllDiscount;
			var products=UnitofWork.Product.GetAll(includeProperties: "productImages").OrderByDescending(p => p.OrderCount)
		.Take(10)
		.ToList();
			ViewBag.BestSellingProducts = products;

			var categories = UnitofWork.Category.GetAll(includeProperties: "products"); // Retrieve all categories

			var topSellingCategories = categories
				.Select(category => new
				{
					Category = category,
					TotalOrderCount = category.products.Sum(product => product.OrderCount) // Calculate total OrderCount for products in each category
				})
				.OrderByDescending(category => category.TotalOrderCount) // Order categories by total OrderCount in descending order
				.Take(10) // Take the top 10 categories
				.Select(category => category.Category) // Select only the category objects
				.ToList();

			ViewBag.BestSellingCategories = topSellingCategories;
			if (startDate.HasValue && endDate.HasValue)
			{
				if (startDate > endDate)
				{
					TempData["errorMessage"] = "Start Date should be less than end Date";
					return View(new List<SalesReport>());
				}
				if (endDate > DateTime.Now)
				{
					TempData["errorMessage"] = "End Date can't be greater than today";
					return View(new List<SalesReport>());

				}

				var orders = UnitofWork.OrderDetails.GetAll();
				var dateeee = orders.FirstOrDefault().DeliveredDate.Date;
				var orderDetails = UnitofWork.OrderDetails.GetAll(d => d.DeliveredDate.Date >= startDate && d.DeliveredDate.Date <= endDate);

				var salesData = orderDetails
					.Where(order => order.ProductStatus == SD.statusDelivered)
		.GroupBy(od => od.DeliveredDate.Date)
		.Select(group => new SalesReport
		{
			Date = group.Key,
			SalesCount = group.Sum(od => od.Count),
			OrderAmount = group.Sum(od => od.Price + od.discountSavedPrice), // Original price plus discount price
			DiscountAmount = group.Sum(od => od.Price),
			DiscountDeduction = group.Sum(od => od.discountSavedPrice)  // Original price minus discount price
																		// Add more properties as needed
		})
		.ToList();
				// Handle dates with no orders (optional)
				var datesWithNoOrders = Enumerable.Range(0, (int)(endDate.Value - startDate.Value).TotalDays + 1)
					.Select(offset => startDate.Value.AddDays(offset))
					.Except(salesData.Select(s => s.Date))
					.ToList();

				// Add entries for dates with no orders
				foreach (var date in datesWithNoOrders)
				{
					salesData.Add(new SalesReport
					{
						Date = date,
						SalesCount = 0,
						OrderAmount = 0,
						DiscountAmount = 0
						// Add more properties as needed
					});
				}

				// Sort the sales data by date
				salesData = salesData.OrderBy(s => s.Date).ToList();



				return View(salesData);

			}
			else
			{   //All Sales Data
				var allSalesData = context.orderDetails
		.Where(od => od.DeliveredDate != DateTime.MinValue && od.ProductStatus==SD.statusDelivered) // Filter out records with default delivery date
		.GroupBy(od => od.DeliveredDate.Date)
		.Select(group => new SalesReport
		{
			Date = group.Key,
			SalesCount = group.Sum(od => od.Count),
			OrderAmount = group.Sum(od => od.Price + od.discountSavedPrice),
			DiscountAmount = group.Sum(od => od.Price),
			// Calculate discount deduction here
			DiscountDeduction = group.Sum(od => od.discountSavedPrice)
			// Add more properties as needed
		})
		.ToList();
				return View(allSalesData);

			}



		}
		[HttpGet]
		public IActionResult GetSalesData(string type, int? year, int? month)
		{
			List<SalesReport> salesData = new List<SalesReport>();

			if (type == "daily")
			{
				// Fetch daily sales data based on year and month
				salesData = Enumerable.Range(1, DateTime.DaysInMonth(year.Value, month.Value))
					.Select(day => new DateTime(year.Value, month.Value, day))
					.GroupJoin(
						UnitofWork.OrderDetails.GetAll(od =>
							od.DeliveredDate.Year == year &&
							od.DeliveredDate.Month == month &&
							od.ProductStatus == SD.statusDelivered),
						date => date.Date,
						od => od.DeliveredDate.Date,
						(date, orders) => new SalesReport
						{
							Date = date,
							SalesCount = orders.Any() ? orders.Sum(od => od.Count) : 0,
							OrderAmount = orders.Any() ? orders.Sum(od => od.Price) : 0,
							DiscountAmount = orders.Any() ? orders.Sum(od => od.Price - od.discountSavedPrice) : 0,
							DiscountDeduction = orders.Any() ? orders.Sum(od => od.discountSavedPrice) : 0
						})
					.OrderBy(s => s.Date)
					.ToList();
			}
			else if (type == "monthly")
			{
				salesData = Enumerable.Range(1, 12)  // Generate a sequence of numbers representing each month
	.Select(month => new {
		Month = month,
		Sales = UnitofWork.OrderDetails.GetAll(od =>
			od.DeliveredDate.Year == year &&
			od.DeliveredDate.Month == month &&
			od.ProductStatus == SD.statusDelivered)
			.GroupBy(od => od.DeliveredDate.Month)
			.Select(group => new SalesReport
			{
				MonthName = new DateTime(year.Value, group.Key, 1).ToString("MMMM"),
				SalesCount = group.Sum(od => od.Count),
				OrderAmount = group.Sum(od => od.Price ),
				DiscountAmount = group.Sum(od => od.Price - od.discountSavedPrice),
				DiscountDeduction = group.Sum(od => od.discountSavedPrice)
			})
			.FirstOrDefault() // Take the first or default group
	})
	.OrderBy(item => item.Month) // Ensure months are ordered
	.Select(item => item.Sales ?? new SalesReport 
	{
		MonthName = new DateTime(year.Value, item.Month, 1).ToString("MMMM"),
		SalesCount = 0,
		OrderAmount = 0,
		DiscountAmount = 0,
		DiscountDeduction = 0
	})
	.ToList();

			}
			else if (type == "yearly")
			{
				// Fetch yearly sales data
				salesData = Enumerable.Range(2020, DateTime.Now.Year - 2020 + 1) // Generate a sequence of years from 2020 to the current year
					.Select(year => new {
						Year = year,
						Sales = UnitofWork.OrderDetails.GetAll(od =>
							od.DeliveredDate.Year == year &&
							od.ProductStatus == SD.statusDelivered)
							.GroupBy(od => od.DeliveredDate.Year)
							.Select(group => new SalesReport
							{
								Year = new DateTime(year, 1, 1),

								SalesCount = group.Sum(od => od.Count),
								OrderAmount = group.Sum(od => od.Price),
								DiscountAmount = group.Sum(od => od.Price - od.discountSavedPrice),
								DiscountDeduction = group.Sum(od => od.discountSavedPrice)
							})
							.FirstOrDefault() // Take the first or default group
					})
					.OrderBy(item => item.Year) // Ensure years are ordered
					.Select(item => item.Sales ?? new SalesReport // Project to SalesReport or create default if null
					{
						Year = new DateTime(item.Year, 1, 1),
					
						SalesCount = 0,
						OrderAmount = 0,
						DiscountAmount = 0,
						DiscountDeduction = 0
					})
					.ToList();

			}

			return Json(salesData);
		}



	}
}
