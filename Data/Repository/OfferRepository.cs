using Eshop.Data.Data;
using Eshop.Model.Models;


namespace Eshop.Data.Repository
{
    public class OfferRepository : Repository<Offer>, IOfferRepository
    {
        private readonly ApplicationDbContext context;

        public OfferRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }


        public void update(Offer offer)
        {
            context.Update(offer);
        }
    }
}
