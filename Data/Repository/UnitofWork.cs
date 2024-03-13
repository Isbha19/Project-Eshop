using Eshop.Data.Data;
using Eshop.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly ApplicationDbContext context;

        public UnitofWork(ApplicationDbContext context)
        {
            this.context = context;
            Category = new CategoryRepository(context);
            Product = new ProductRepository(context);
            ProductImage=new ProductImageRepository(context);
            Colors=new ColorsRepository(context);
            Cart=new CartRepository(context);
            Address=new AddressRepository(context);
			OrderDetails=new OrderDetailsRepository(context);
            OrderHeader=new OrderHeaderRepository(context);
            User=new UserRepository(context);
			Wishlist=new WishlistRepository(context);
            Coupon=new CouponRepository(context);
            Wallet=new WalletRepository(context);
            WalletHeader=new WalletHeaderRepository(context);
            Offer=new OfferRepository(context);
            ProductOffer=new ProductOfferRepository(context);
            CategoryOffer=new CategoryOfferRepository(context);
		}

        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }

        public IProductImageRepository ProductImage { get; private set; }
        public IColorsRepository Colors { get; private set; }
      public ICartRepository Cart { get; private set; }
        public IAddressRepository Address { get; private set; }
        public IOrderDetailsRepository OrderDetails { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IUserRepository User { get; private set; }
        public IWishlistRepository Wishlist { get; private set; }
        public ICouponRepository Coupon { get; private set; }
        public IWalletRepository Wallet { get; private set; }
        public IWalletHeaderRepository WalletHeader { get; private set; }
        public IOfferRepository Offer { get; private set; }
        public IProductOfferRepository ProductOffer { get; private set; }
        public ICategoryOfferRepository CategoryOffer { get; private set; }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
