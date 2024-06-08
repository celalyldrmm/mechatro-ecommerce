using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mechatro_ecommerce.Models;
using System.Linq;

namespace mechatro_project.Controllers
{

    public class AdminController : Controller
    {
		ICategoryRepository categoryRepository = new CategoryRepository();
		UserRepository u = new UserRepository();
        CategoryRepository c = new CategoryRepository();
        SupplierRepository s = new SupplierRepository();     	        
        MechatroContext context = new MechatroContext();


		[HttpGet]

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string usernameOrEmail, string password)
        {
            User? user = await u.loginControl(usernameOrEmail, password);

            if (user != null)
            {
                
                HttpContext.Session.SetString("LoginInfo", usernameOrEmail);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "Kullanıcı adı, e-posta veya şifre yanlış";
                return View();
            }
        }

		public IActionResult Logout()
		{
			HttpContext.Session.Remove("LoginInfo");
            HttpContext.Session.Remove("Admin");
			return RedirectToAction("Login");
		}




		public ActionResult Index()
		{
			MechatroContext context = new MechatroContext();
			AdminViewModel avm = new AdminViewModel();

			avm.Categories = context.Categories.ToList();
			avm.Suppliers = context.Suppliers.ToList();
            return View(avm);
        }

    

        public async Task<IActionResult> CategoryIndex()
		{
			List<Category> categories = await c.CategorySelect();
			return View(categories);
		}


		[HttpGet]
		public IActionResult CategoryCreate()
		{
			CategoryFill();
			return View();
		}
		void CategoryFill()
		{
			List<Category> categories = c.CategorySelectMain();
			ViewData["categoryList"] = categories.Select(c => new SelectListItem { Text = c.CategoryName, Value = c.CategoryID.ToString()});
		}

		[HttpPost]
		public IActionResult CategoryCreate(Category category)
		{

			bool answer = categoryRepository.CategoryInsert(category);
			if (answer == true)
			{
				TempData["Message"] = category.CategoryName + " Eklendi";
			}
			else
			{
				TempData["Message"] = "HATA";
			}
			return RedirectToAction(nameof(CategoryCreate));
		}

		[HttpGet]
		public async Task<IActionResult> CategoryEdit(int? id)
		{
			CategoryFill();
			if (id == null || context.Categories == null)
			{
				return NotFound();
			}
			var category = await c.CategoryDetails(id);
			return View(category);
		}

		[HttpPost]
        public IActionResult CategoryEdit(Category category)
        {

            bool answer = c.CategoryUpdate(category);
            if (answer == true)
            {
                TempData["Message"] = category.CategoryName + " Güncellendi";
                return RedirectToAction("CategoryIndex");
            }
            else
            {
                TempData["Message"] = " hata ";
            }
            return RedirectToAction(nameof(CategoryEdit));
        }

        public async Task<IActionResult> CategoryDetails(int? id)
		{
			var category = await context.Categories.FirstOrDefaultAsync(c => c.CategoryID == id);
			ViewBag.categoryname = category?.CategoryName;

			return View(category);
		}

		public async Task<IActionResult> CategoryDelete(int? id)
		{
			if (id == null || context.Categories == null)
			{
				return NotFound();
			}
			var category = await context.Categories.FirstOrDefaultAsync(c => c.CategoryID == id);

			if (category == null)
			{
				return NotFound();
			}

			return View(category);
		}

		[HttpPost, ActionName("CategoryDelete")] 
		public async Task<IActionResult> CategoryDeleteConfirmed(int id)
		{
			bool answer = c.CategoryDelete(id);
			if (answer == true)
			{
				TempData["Message"] = "Silindi";
				return RedirectToAction("CategoryIndex");
			}
			else
			{
				TempData["Message"] = "HATA";
				return RedirectToAction(nameof(CategoryDelete));
			}
		}

		public async Task<IActionResult> SupplierIndex()
		{
			List<Supplier> suppliers = await s.SupplierSelect();
			return View(suppliers);
		}

		[HttpGet]
		public IActionResult SupplierCreate()
		{
			return View();
		}

		[HttpPost]
		public IActionResult SupplierCreate(Supplier supplier)
		{


			bool answer = s.SupplierInsert(supplier);
			if (answer == true)
			{
				TempData["Message"] = supplier.BrandName + " Markası Eklendi";
			}
			else
			{
				TempData["Message"] = "HATA " + supplier.BrandName + " Markası eklenemedi.";
			}
			return RedirectToAction(nameof(SupplierCreate)); 
		}

		[HttpGet]
		public async Task<IActionResult> SupplierEdit(int? id)
		{
			if (id == null || context.Suppliers == null)
			{
				return NotFound();
			}

			var supplier = await s.SupplierDetails(id);

			return View(supplier);
		}

		[HttpPost]
		public IActionResult SupplierEdit(Supplier supplier)
		{
			if (supplier.PhotoPath == null)
			{
				string? PhotoPath = context.Suppliers.FirstOrDefault(s => s.SupplierID == supplier.SupplierID).PhotoPath;
				supplier.PhotoPath = PhotoPath;
			}

			bool answer = s.SupplierUpdate(supplier);
			if (answer == true)
			{
				TempData["Message"] = "Güncellendi";
				return RedirectToAction("SupplierIndex");
			}
			else
			{
				TempData["Message"] = "HATA";
				return RedirectToAction(nameof(SupplierEdit));
			}
		}

		[HttpGet]
		public async Task<IActionResult> SupplierDetails(int? id)
		{
			var supplier = await context.Suppliers.FirstOrDefaultAsync(c => c.SupplierID == id);
			ViewBag.brandname = supplier?.BrandName;

			return View(supplier);
		}

		[HttpGet]
		public async Task<IActionResult> SupplierDelete(int? id)
		{
			if (id == null || context.Suppliers == null)
			{
				return NotFound();
			}

			var supplier = await context.Suppliers.FirstOrDefaultAsync(c => c.SupplierID == id);

			if (supplier == null)
			{
				return NotFound();
			}

			return View(supplier);
		}

		[HttpPost, ActionName("SupplierDelete")] 
		public async Task<IActionResult> SupplierDeleteConfirmed(int id)
		{
			bool answer = s.SupplierDelete(id);
			if (answer == true)
			{
				TempData["Message"] = "Silindi";
				return RedirectToAction("SupplierIndex");
			}
			else
			{
				TempData["Message"] = "HATA";
				return RedirectToAction(nameof(SupplierDelete));
			}
		}

		

		

















    }
}
