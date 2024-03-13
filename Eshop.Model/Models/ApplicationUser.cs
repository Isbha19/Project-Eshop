using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Identity;
using Microsoft.AspNetCore.Authentication;

namespace Eshop.Model.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Required]
		public string? Name { get; set; } = string.Empty;
		public string? Gender { get; set; }
		public DateTime DateOfBirth { get; set; } 
		public string? Location { get; set; }
		public string? AlternatePhoneNum { get; set; }
		public bool isRefferalFlag {  get; set; }
		public string? ReferralCode { get; set; }
    

    }

}