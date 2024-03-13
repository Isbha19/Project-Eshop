using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Model.Models
{
	public class OrderHeader
	{
        public int Id { get; set; }
        public  string? UserId { get; set; }
		[ValidateNever]
		[ForeignKey("UserId")]
		public ApplicationUser applicationUser { get; set; }
		public List<OrderDetails> orderDetails { get; set; }
        public string? PaymentType { get; set; } = string.Empty;
        public double OrderTotal { get; set; }
        public double ShippingCharge { get; set; }
        public bool isShipped { get; set; }
        public DateTime OrderDate { get; set; }
       public DateTime DeliveredDate { get; set; }


        public DateTime PaymentTime { get; set; }
        public DateTime ShippingDate { get; set; }
		
		public string? OrderStatus { get; set; }
        public  string? PaymentStatus { get; set; }
        public string? TrackingNumber { get; set; }
        public string? SessionId { get; set; }
        public string? PaymentIntendId { get; set; }

        public string? Name { get; set; }
   
        public string? Address { get; set; }
		public double savedPrice { get; set; }
		public string? City { get; set; }
        public string? State { get; set; }
		public string? Pincode { get; set; }
		public string? saveAddress { get; set; }
        public double DiscountAmountApplied {  get; set; }
		public string? PhoneNumber { get; set; }

    }
}
