using Eshop.Model.Models;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Model.ViewModels
{
    public class ProductOfferViewModel
    {
        public IEnumerable<SelectListItem>? productList { get; set; }
        public IEnumerable<SelectListItem>? offerList { get; set; }

        public ProductOffer? productOffer{ get; set; }
        

	}
}
