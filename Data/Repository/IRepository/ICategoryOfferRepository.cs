using Eshop.Model.Models;




namespace Eshop.Data.Repository
{
    public interface ICategoryOfferRepository : IRepository<CategoryOffer>
    {
        void update(CategoryOffer categoryOffer);


    }
}
