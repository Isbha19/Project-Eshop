using Eshop.Data.Data;
using Eshop.Model.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.Services
{
    public class CouponExpirationService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public CouponExpirationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    // Query coupons with expiry date in the past
                    var expiredCoupons = dbContext.coupons.Where(c => c.ExpireDate < DateOnly.FromDateTime(DateTime.Now) && c.isActive).ToList();

                    foreach (var coupon in expiredCoupons)
                    {
                        coupon.isActive = false;
                        dbContext.coupons.Update(coupon);
                    }

                    dbContext.SaveChanges(); // Save changes to the database
                }

                // Delay before the next execution (e.g., every 24 hours)
                await Task.Delay(TimeSpan.FromHours(2), stoppingToken);
            }
        }
    }
}
