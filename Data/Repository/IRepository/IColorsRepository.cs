using Eshop.Model.Models;




namespace Eshop.Data.Repository
{
    public interface IColorsRepository : IRepository<Colors>
    {
        void update(Colors colors);


    }
}
