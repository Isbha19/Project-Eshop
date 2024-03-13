using Eshop.Data.Data;
using Eshop.Model.Models;


namespace Eshop.Data.Repository
{
    public class WalletRepository : Repository<Wallet>, IWalletRepository
    {
        private readonly ApplicationDbContext context;

        public WalletRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }


        public void update(Wallet wallet)
        {
            context.Update(wallet);
        }
    }
}
