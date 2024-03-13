using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Eshop.Model.Models
{
	public class OrderDetails
	{
        public int Id { get; set; }
        public int OrderId { get; set; }
		[ValidateNever]
		[ForeignKey("OrderId")]
		public OrderHeader? orderHeader { get; set; }
        public int ProductId { get; set; }
        [ValidateNever]
        [ForeignKey("ProductId")]
        public Product? products { get; set; }
        public int Count { get; set; }

		public DateTime DeliveredDate { get; set; }
        public bool ReturnPolicyValid { get; set; } = false;

        public double Price { get; set; }
        public bool IsReturned { get; set; }
        public string? ProductStatus { get; set; }
        public string? ProductPaymentStatus { get; set; }
        public  double  CouponDiscountPrice { get; set; }

        public double discountSavedPrice { get; set; }
    }
	
}
