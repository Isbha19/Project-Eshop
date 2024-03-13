using Eshop.Model.Models;




namespace Eshop.Data.Repository
{
    public interface IWalletHeaderRepository : IRepository<WalletHeader>
    {
        void update(WalletHeader walletHeader);


    }
}
