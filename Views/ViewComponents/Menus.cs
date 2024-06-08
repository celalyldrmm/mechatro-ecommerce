using mechatro_ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace mechatro_project.Views.ViewComponents
{
    public class Menus : ViewComponent
    {
        MechatroContext context = new MechatroContext();

        public IViewComponentResult Invoke()
        {
            List<Category> categories = context.Categories.ToList();
            return View(categories);
        }


    }
}


