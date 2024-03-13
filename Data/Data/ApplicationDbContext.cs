using Eshop.Model.Models;
using Eshop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> productImages { get; set; }
        public DbSet<Colors> colors { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }
        public DbSet<ShoppingCart> shoppingCarts { get; set; }
        public DbSet<ShippingAdress> shippingAdresses { get; set; }
        public DbSet<OrderHeader> orderHeaders { get; set; }
        public DbSet<OrderDetails> orderDetails { get; set; }
		public DbSet<Wishlist> wishlists { get; set; }
        public DbSet<Coupon> coupons { get; set; }
        public DbSet<Wallet> wallets { get; set; }
        public DbSet<WalletHeader> walletsHeader { get; set; }
        public DbSet<Offer> offers { get; set; }
        public DbSet<ProductOffer> productoffers { get; set; }
        public DbSet<CategoryOffer> categoryOffers { get; set; }
  
		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Offer>().HasData(
     new Offer { OfferId = 1, OfferName = "Discount 1", offerType = Offer.OfferType.Percentage, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(30), IsActive = true },
     new Offer { OfferId = 2, OfferName = "Discount 2", offerType = Offer.OfferType.Percentage, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(60), IsActive = true },
     new Offer { OfferId = 3, OfferName = "Discount 3", offerType = Offer.OfferType.FixedAmount, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(90), IsActive = true }
 );





        }
    }
}
