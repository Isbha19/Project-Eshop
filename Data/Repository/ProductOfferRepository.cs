using Eshop.Data.Data;
using Eshop.Model.Models;


namespace Eshop.Data.Repository
{
    public class ProductOfferRepository : Repository<ProductOffer>, IProductOfferRepository
    {
        private readonly ApplicationDbContext context;

        public ProductOfferRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }


        public void update(ProductOffer productOffer)
        {
            context.Update(productOffer);
        }
    }
}
