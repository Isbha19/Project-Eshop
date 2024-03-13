using Eshop.Model.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Model.ViewModels
{
    public class UserViewModel
    {
        public ApplicationUser appUser { get; set; }
        public List<SelectListItem> GenderList {  get; set; }
    }
}
