using Eshop.Model.Models;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Model.ViewModels
{
    public class OrderViewModel
    {
        public IEnumerable<ShippingAdress>? ShippingAdresses { get; set; }
        public IEnumerable<ShoppingCart>? shoppingCarts { get; set; }
		public IEnumerable<Coupon>? coupons { get; set; }
		public IEnumerable<Coupon>? FilteredCoupon { get; set; }

		public double OrderTotal { get; set; }
		public double OrderTotalWithDiscount { get; set; }
		public bool offered { get; set; }
		public double SavedPrice { get; set; }
        public int ShipCharge { get; set; }
        public double OriginalPrice { get; set; }
        public OrderHeader? orderHeader{ get; set; }
        public int stockTotal { get; set; }
        public bool FromWishlist { get; set; }=false;
		public WalletHeader walletHeader {  get; set; }
		public ApplicationUser user { get; set; }

	}
}
