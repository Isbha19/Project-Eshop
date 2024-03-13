using Eshop.Model.Models;




namespace Eshop.Data.Repository
{
    public interface IWalletRepository : IRepository<Wallet>
    {
        void update(Wallet wallet);


    }
}
