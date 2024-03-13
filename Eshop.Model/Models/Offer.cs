using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Model.Models
{

    public class Offer
    {
        public int OfferId { get; set; }
        public string OfferName { get; set; }
        public OfferType offerType { get; set; }

        public double Discount
        {  get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public enum OfferType
        {
            Percentage,
            FixedAmount
        }


        // Navigation properties
        //public ICollection<CategoryOffer> CategoryOffers { get; set; }
        //public ICollection<ProductOffer> ProductOffers { get; set; }
    }

}
