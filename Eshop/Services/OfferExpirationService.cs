using Eshop.Data.Data;
using Eshop.Model.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Eshop.Data.Repository;
using Eshop.Utility;

namespace Eshop.Services
{
    public class OfferExpirationService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUnitofWork unitofWork;

        public OfferExpirationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var unitofWork = scope.ServiceProvider.GetRequiredService<IUnitofWork>();

                    var expiredOffer = unitofWork.Offer
                        .GetAll(u => u.EndDate < DateTime.Now && u.IsActive)
                        .ToList();


                    foreach (var offer in expiredOffer)
                    {
                        offer.IsActive = false;
                        unitofWork.Offer.update(offer);
                        unitofWork.Save();
                        var productWithOffers = unitofWork.ProductOffer.GetAll(u => u.OfferId == offer.OfferId, includeProperties: "product");
                        if (productWithOffers != null)
                        {
                            foreach (var item in productWithOffers)
                            {
                                item.product.isOffered = false;
                                item.product.OfferPrice = 0;
                                item.product.OfferName = "";
                                item.product.OfferType = "";
                                unitofWork.Product.updateOffer(item.product);
                            }
                            unitofWork.Save();
                        }
                        var categoryWithOffers = unitofWork.CategoryOffer.GetAll(u => u.OfferId == offer.OfferId, includeProperties: "category,category.products");
                        if (categoryWithOffers != null)
                        {
                            foreach (var item in categoryWithOffers)
                            {
                                item.category.IsDiscount = false;
                                unitofWork.Category.update(item.category);
                                unitofWork.Save();
                                foreach (var product in item.category.products)
                                {
                                    if (product.OfferType != SD.ProductOffer)
                                    {
                                        product.isOffered = false;
                                        product.OfferPrice = 0;
                                        product.OfferName = "";
                                        product.OfferType = "";
                                        unitofWork.Product.updateOffer(product);
                                        unitofWork.Save();
                                    }
                                }

                            }
                        }

                    }

                }
                // Delay before the next execution (e.g., every 24 hours)
                await Task.Delay(TimeSpan.FromHours(2), stoppingToken);
            }
        }
    }
}
