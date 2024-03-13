using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Model.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The code is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "The code must be between 6 and 20 characters.")]
        public string? Code { get; set; }
        [Required(ErrorMessage = "The percentage is required.")]
        [Range(0, 100, ErrorMessage = "The percentage must be between 0 and 100.")]
        public double Percentage { get; set; }
        public DateOnly ExpireDate { get; set; }
        public bool isActive { get; set; } = true;
        public bool isReferral { get; set; } 
        public bool Validate()
        {

            if (ExpireDate < DateOnly.FromDateTime(DateTime.Today))
            {
                return false; // Coupon has expired
            }

            return true; // Coupon is valid
        }

    }


}
