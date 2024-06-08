using mechatro_ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
namespace mechatro_project.Views.ViewComponents
{
	public class Footer : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}