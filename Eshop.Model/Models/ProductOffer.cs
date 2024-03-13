using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Model.Models
{
	public class ProductOffer
	{
        public int Id { get; set; }
		public int OfferId { get; set; }
		[ValidateNever]
		[ForeignKey("OfferId")]
		public Offer offer { get; set; }
		public int ProductId { get; set; }
		[ValidateNever]
		[ForeignKey("ProductId")]
		public Product product { get; set; }

	}
}
