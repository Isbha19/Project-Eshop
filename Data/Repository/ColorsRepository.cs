using Eshop.Data.Data;
using Eshop.Model.Models;


namespace Eshop.Data.Repository
{
    public class ColorsRepository : Repository<Colors>, IColorsRepository
    {
        private readonly ApplicationDbContext context;

        public ColorsRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }


        public void update(Colors colors)
        {
            context.Update(colors);
        }
    }
}
