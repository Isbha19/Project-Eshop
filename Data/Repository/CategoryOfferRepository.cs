using Eshop.Data.Data;
using Eshop.Model.Models;


namespace Eshop.Data.Repository
{
    public class CategoryOfferRepository : Repository<CategoryOffer>, ICategoryOfferRepository
    {
        private readonly ApplicationDbContext context;

        public CategoryOfferRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }


        public void update(CategoryOffer categoryOffer)
        {
            context.Update(categoryOffer);
        }
    }
}
