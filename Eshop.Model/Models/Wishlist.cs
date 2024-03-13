using Eshop.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Model.Models
{
	public class Wishlist
	{
        public int Id { get; set; }
        public string UserId { get; set; }
        public int productId { get; set; }
		[ForeignKey("productId")]
		[ValidateNever]
		public Product? product { get; set; }
    }
}
