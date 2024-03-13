using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Model.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int productId { get; set; }
        [ForeignKey("productId")]
        [ValidateNever]
        public Product? products { get; set; }
        public int Count { get; set; }
        public double ShippingCharge { get; set; }
        public double TotalPrice { get; set; }
        public double CouponDiscountPrice { get; set; }
        public double CartItemOfferPrice { get; set; } 
        public bool CouponApplied { get; set; } = false;
		

	}
}