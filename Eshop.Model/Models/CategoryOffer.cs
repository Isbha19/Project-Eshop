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
	public class CategoryOffer
	{
        public int Id { get; set; }
		public int OfferId { get; set; }
		[ValidateNever]
		[ForeignKey("OfferId")]
		public Offer offer { get; set; }
		public int CategoryId { get; set; }
		[ValidateNever]
		[ForeignKey("CategoryId")]
		public Category category { get; set; }

	}
}
