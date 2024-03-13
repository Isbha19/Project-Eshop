using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Repository
{
    public interface IUnitofWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        IProductImageRepository ProductImage { get; }
        IColorsRepository Colors { get; }
        ICartRepository Cart { get; }
        IAddressRepository Address { get; }
     IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailsRepository OrderDetails { get; }
    
        IUserRepository User { get; }
        IWishlistRepository Wishlist { get; }
        ICouponRepository Coupon { get; }
        IWalletRepository Wallet { get; }
        IWalletHeaderRepository WalletHeader { get; }
        IOfferRepository Offer { get; }
        IProductOfferRepository ProductOffer { get; }
        ICategoryOfferRepository CategoryOffer { get; }
        void Save();
    }
}
