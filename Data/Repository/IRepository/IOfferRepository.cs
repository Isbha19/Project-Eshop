using Eshop.Model.Models;




namespace Eshop.Data.Repository
{
    public interface IOfferRepository : IRepository<Offer>
    {
        void update(Offer offer);


    }
}
