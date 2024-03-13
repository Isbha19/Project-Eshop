using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Eshop.Model.Models;
using Eshop.Data.Data;
using Eshop.Data.Repository;

public class ExpiredOfferCleanupService : BackgroundService
{
	private readonly IServiceScopeFactory _serviceScopeFactory;

	public ExpiredOfferCleanupService(IServiceScopeFactory serviceScopeFactory)
	{
		_serviceScopeFactory = serviceScopeFactory;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			using (var scope = _serviceScopeFactory.CreateScope())
			{
				var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitofWork>();

				// Check for and stop expired offers
				await StopExpiredCoupons(unitOfWork);
				await StopExpiredOffer(unitOfWork);

				// Wait for the specified interval before running the task again
				await Task.Delay(TimeSpan.FromHours(2), stoppingToken); // Adjust the interval as needed
			}
		}
	}

	private async Task StopExpiredCoupons(IUnitofWork unitOfWork)
	{
		var coupons = unitOfWork.Coupon.GetAll();
		foreach (var coupon in coupons)
		{
			if (coupon.ExpireDate < DateOnly.FromDateTime(DateTime.Now))
			{
				coupon.isActive = false;
				unitOfWork.Coupon.update(coupon);
				unitOfWork.Save();
			}
		}
		
	}
    private async Task StopExpiredOffer(IUnitofWork unitOfWork)
    {
		var offer=unitOfWork.Offer.GetAll();
        foreach (var item in offer)
        {
            if (item.EndDate < DateTime.Now)
            {
                item.IsActive = false;
                unitOfWork.Offer.update(item);
                unitOfWork.Save();
            }
        }

    }
}
