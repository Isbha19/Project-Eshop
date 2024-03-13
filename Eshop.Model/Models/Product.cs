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
    public class Product
    {
        public int Id { get; set; }
        public  string? ProductName { get; set; }
        public string? ProductDescription { get; set;}

        public double ProductPrice { get; set;}
        public int Quantity { get; set; }
        public int StockLeft { get; set; }
        public List<ProductImage>? productImages { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category? category { get; set; }
        
        public int ColorsId { get; set; }
        [ForeignKey("ColorsId")]
        public Colors? colors { get; set; }
        public int Rating { get; set; }
        public int ViewsCount { get; set; } = 0;
        public bool isFeatured { get; set; } = false;
		[ValidateNever]

		public bool isOffered { get; set; } = false;
		[ValidateNever]

		public double OfferPrice { get; set; }
		[ValidateNever]
		public string? OfferName { get; set; } = string.Empty;
		[ValidateNever]

		public string? OfferType { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; }

        public int OrderCount { get; set; } = 0;
        public bool isWishlisted { get; set; } = false;
    }
}
