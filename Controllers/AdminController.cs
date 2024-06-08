using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mechatro_ecommerce.Models;
using System.Linq;

namespace mechatro_ecommerce.Controllers
{

    public class AdminController : Controller
    {
		ICategoryRepository categoryRepository = new CategoryRepository();
		UserRepository u = new UserRepository();
        CategoryRepository c = new CategoryRepository();
        SupplierRepository s = new SupplierRepository();
        ProductRepository p = new ProductRepository();
        StatusRepository st = new StatusRepository();
        SettingRepository setting = new SettingRepository();


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

        public async Task<IActionResult> StatusIndex()
        {
            List<Status> statuses = await st.StatusSelect();
            return View(statuses);
        }

        [HttpGet]
        public IActionResult StatusCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult StatusCreate(Status status)
        {

            bool answer = st.StatusInsert(status);
            if (answer == true)
            {
                TempData["Message"] = status.StatusName + " Statüsü Eklendi";
            }
            else
            {
                TempData["Message"] = "HATA " + status.StatusName + " Statüsü eklenemedi.";
            }
            return RedirectToAction(nameof(StatusCreate));
        }

        [HttpGet]
        public async Task<IActionResult> StatusEdit(int? id)
        {
            if (id == null || context.Statuses == null)
            {
                return NotFound();
            }

            var status = await st.StatusDetails(id);

            return View(status);
        }

        [HttpPost]
        public IActionResult StatusEdit(Status status)
        {

            bool answer = st.StatusUpdate(status);
            if (answer == true)
            {
                TempData["Message"] = "Güncellendi";
                return RedirectToAction("StatusIndex");
            }
            else
            {
                TempData["Message"] = "HATA";
                return RedirectToAction(nameof(StatusEdit));
            }
        }

        [HttpGet]
        public async Task<IActionResult> StatusDelete(int? id)
        {
            if (id == null || context.Statuses == null)
            {
                return NotFound();
            }

            var status = await context.Statuses.FirstOrDefaultAsync(c => c.StatusID == id);

            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        [HttpPost, ActionName("StatusDelete")]
        public async Task<IActionResult> StatusDeleteConfirmed(int id)
        {
            bool answer = st.StatusDelete(id);
            if (answer == true)
            {
                TempData["Message"] = "Silindi";
                return RedirectToAction("StatusIndex");
            }
            else
            {
                TempData["Message"] = "HATA";
                return RedirectToAction(nameof(StatusDelete));
            }
        }

        public async Task<IActionResult> ProductIndex()
        {
            List<Product> products = await p.ProductSelect();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> ProductCreate()
        {
            List<Category> categories = await c.CategorySelect();
            ViewData["categoryList"] = categories.Select(c => new SelectListItem { Text = c.CategoryName, Value = c.CategoryID.ToString() });

            List<Supplier> suppliers = await s.SupplierSelect();
            ViewData["supplierList"] = suppliers.Select(s => new SelectListItem { Text = s.BrandName, Value = s.SupplierID.ToString() });

            List<Status> statuses = await st.StatusSelect();
            ViewData["statusList"] = statuses.Select(s => new SelectListItem { Text = s.StatusName, Value = s.StatusID.ToString() });

            return View();
        }

        [HttpPost]
        public IActionResult ProductCreate(Product product)
        {
            bool answer = p.ProductInsert(product);
            if (answer == true)
            {
                TempData["Message"] = product.ProductName + " Eklendi";
            }
            else
            {
                TempData["Message"] = "HATA";
            }
            return RedirectToAction(nameof(ProductCreate));
        }

        public async Task<IActionResult> ProductEdit(int? id)
        {
            CategoryFill();
            SupplierFill();
            StatusFill();

            List<Category> categories = await c.CategorySelect();
            ViewData["categoryList"] = categories.Select(c => new SelectListItem { Text = c.CategoryName, Value = c.CategoryID.ToString() });

            if (id == null || context.Products == null)
            {
                return NotFound();
            }

            var product = await p.ProductDetails(id);

            return View(product);
        }

        [HttpPost]
        public IActionResult ProductEdit(Product product)
        {
            Product prd = context.Products.FirstOrDefault(s => s.ProductID == product.ProductID);
            product.AddDate = prd.AddDate;


            if (product.PhotoPath == null)
            {
                string? PhotoPath = context.Products.FirstOrDefault(s => s.ProductID == product.ProductID).PhotoPath;
                product.PhotoPath = PhotoPath;
            }

            bool answer = p.ProductUpdate(product);
            if (answer == true)
            {
                TempData["Message"] = "Güncellendi";
                return RedirectToAction("ProductIndex");
            }
            else
            {
                TempData["Message"] = "HATA";
                return RedirectToAction(nameof(ProductEdit));
            }
        }

        async void SupplierFill()
        {
            List<Supplier> suppliers = await s.SupplierSelect();
            ViewData["supplierList"] = suppliers.Select(s => new SelectListItem { Text = s.BrandName, Value = s.SupplierID.ToString() });
        }

        async void StatusFill()
        {
            List<Status> statuses = await st.StatusSelect();
            ViewData["statusList"] = statuses.Select(s => new SelectListItem { Text = s.StatusName, Value = s.StatusID.ToString() });
        }

        public async Task<IActionResult> ProductDetails(int? id)
        {
            var product = await context.Products.FirstOrDefaultAsync(c => c.ProductID == id);
            ViewBag.productname = product?.ProductName;

            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> ProductDelete(int? id)
        {
            if (id == null || context.Products == null)
            {
                return NotFound();
            }

            var product = await context.Products.FirstOrDefaultAsync(c => c.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpPost, ActionName("ProductDelete")]
        public async Task<IActionResult> ProductDeleteConfirmed(int id)
        {
            bool answer = p.ProductDelete(id);
            if (answer == true)
            {
                TempData["Message"] = "Silindi";
                return RedirectToAction("ProductIndex");
            }
            else
            {
                TempData["Message"] = "HATA";
                return RedirectToAction(nameof(ProductDelete));
            }
        }
        public async Task<IActionResult> SettingIndex()
        {
            List<Setting> settings = await setting.SettingSelect();
            return View(settings);
        }

        [HttpGet]
        public IActionResult SettingCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SettingCreate(Setting settings)
        {

            bool answer = setting.SettingInsert(settings);
            if (answer == true)
            {
                TempData["Message"] = settings.Email + " Email Eklendi";
                TempData["Message"] = settings.Telephone + " Telephone Eklendi";
                TempData["Message"] = settings.Address + " Address Eklendi";
            }
            else
            {
                TempData["Message"] = "HATA " + settings.Email + " Email eklenemedi";
                TempData["Message"] = "HATA " + settings.Telephone + " Telephone eklenemedi";
                TempData["Message"] = "HATA " + settings.Address + " Address eklenemedi";

            }
            return RedirectToAction(nameof(SettingCreate));
        }

        [HttpGet]
        public async Task<IActionResult> SettingEdit(int? id)
        {
            if (id == null || context.Settings == null)
            {
                return NotFound();
            }

            var settings = await setting.SettingDetails(id);

            return View(settings);
        }

        [HttpPost]
        public IActionResult SettingEdit(Setting settings)
        {

            bool answer = setting.SettingUpdate(settings);
            if (answer == true)
            {
                TempData["Message"] = "Güncellendi";
                return RedirectToAction("SettingIndex");
            }
            else
            {
                TempData["Message"] = "HATA";
                return RedirectToAction(nameof(SettingEdit));
            }
        }

        [HttpGet]
        public async Task<IActionResult> SettingDelete(int? id)
        {
            if (id == null || context.Settings == null)
            {
                return NotFound();
            }

            var setting = await context.Settings.FirstOrDefaultAsync(s => s.SettingID == id);

            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        [HttpPost, ActionName("SettingDelete")]
        public async Task<IActionResult> SettingDeleteConfirmed(int id)
        {
            bool answer = setting.SettingDelete(id);
            if (answer == true)
            {
                TempData["Message"] = "Silindi";
                return RedirectToAction("SettingIndex");
            }
            else
            {
                TempData["Message"] = "HATA";
                return RedirectToAction(nameof(SettingDelete));
            }
        }

    }
}
