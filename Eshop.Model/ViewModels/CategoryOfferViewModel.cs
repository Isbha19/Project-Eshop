using Eshop.Model.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Model.ViewModels
{
    public class CategoryOfferViewModel
    {
        public IEnumerable<SelectListItem>? categoryList { get; set; }
        public IEnumerable<SelectListItem>? offerList { get; set; }

        public CategoryOffer? categoryOffer { get; set; }

    }
}
