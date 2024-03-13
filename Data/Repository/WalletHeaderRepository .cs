using Eshop.Data.Data;
using Eshop.Model.Models;


namespace Eshop.Data.Repository
{
    public class WalletHeaderRepository : Repository<WalletHeader>, IWalletHeaderRepository
    {
        private readonly ApplicationDbContext context;

        public WalletHeaderRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }


        public void update(WalletHeader walletHeader)
        {
            context.Update(walletHeader);
        }
    }
}
