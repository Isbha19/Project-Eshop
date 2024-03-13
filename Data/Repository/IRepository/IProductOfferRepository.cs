using Eshop.Model.Models;




namespace Eshop.Data.Repository
{
    public interface IProductOfferRepository : IRepository<ProductOffer>
    {
        void update(ProductOffer productOffer);


    }
}
