using Eshop.Models;
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
	public class ShippingAdress
	{
        public int Id { get; set; }
		public string? UserId { get; set; }
		[ForeignKey("UserId")]
		[ValidateNever]
		public ApplicationUser? applicationUser { get; set; }
		[Required]
		public string? Name { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Invalid phone number")]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Pin code is required")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Invalid pin code")]
        public string? PinCode { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string? City { get; set; }


        [Required(ErrorMessage = "District is required")]
        public string? District { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string? State { get; set; }

        [Required(ErrorMessage = "Please specify the type of address")]
        public string? saveAddress { get; set; }
        public bool IsDefault { get; set; } = false;
    }
}
