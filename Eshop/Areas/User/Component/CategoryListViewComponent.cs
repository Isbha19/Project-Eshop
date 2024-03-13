using Eshop.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Eshop.Areas.User.Component
{
    public class CategoryListViewComponent : ViewComponent
    {
        private readonly IUnitofWork unitofwork;

        public CategoryListViewComponent(IUnitofWork unitofWork)
        {
            unitofwork = unitofWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var category = unitofwork.Category.GetAll();
            return View(category);
        }
    }
}
