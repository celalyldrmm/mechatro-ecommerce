using mechatro_ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace mechatro_ecommerce.Controllers
{
    public class HomeController : Controller
    {

        UserRepository userRepository = new UserRepository();
        ProductRepository productRepository = new ProductRepository();
        MainPageModel mpm = new MainPageModel();
        MechatroContext context = new MechatroContext();
        OrderRepository orderRepository = new OrderRepository();
        int mainpageCount = 0;

        public HomeController()

        {
            this.mainpageCount = context.Settings.FirstOrDefault(s => s.SettingID == 1).MainpageCount;
        }
        public IActionResult Index()
        {
            mpm.SliderProducts = productRepository.ProductSelect("Slider", mainpageCount, "Ana", 0);
            mpm.NewProducts = productRepository.ProductSelect("New", mainpageCount, "Ana", 0);
            mpm.TopsellerProducts = productRepository.ProductSelect("Topseller", mainpageCount, "Ana", 0);
            mpm.FeaturedProducts = productRepository.ProductSelect("Featured", mainpageCount, "Ana", 0);

            return View(mpm);

        }
        public IActionResult NewProducts()
        {

            mpm.NewProducts = productRepository.ProductSelect("New", mainpageCount, "New", 0);
            return View(mpm);
        }

        public PartialViewResult _PartialNewProducts(string pageno)
        {
            int pagenumber = Convert.ToInt32(pageno);
            mpm.NewProducts = productRepository.ProductSelect("New", mainpageCount, "New", pagenumber);
            return PartialView(mpm);
        }

        public IActionResult FeaturedProducts()
        {
            mpm.FeaturedProducts = productRepository.ProductSelect("Featured", mainpageCount, "Featured", 0);
            return View(mpm);
        }

        public PartialViewResult _PartialFeaturedProducts(string pageno)
        {
            int pagenumber = Convert.ToInt32(pageno);
            mpm.FeaturedProducts = productRepository.ProductSelect("Featured", mainpageCount, "Featured", pagenumber);
            return PartialView(mpm);
        }
        public IActionResult TopsellerProducts()
        {
            mpm.TopsellerProducts = productRepository.ProductSelect("TopsellerProducts", mainpageCount, "TopsellerProducts", 0);
            return View(mpm);
        }

        public PartialViewResult _PartialTopsellerProducts(string pageno)
        {
            int pagenumber = Convert.ToInt32(pageno);
            mpm.TopsellerProducts = productRepository.ProductSelect("TopsellerProducts", mainpageCount, "TopsellerProducts", pagenumber);
            return PartialView(mpm);
        }
        public async Task<IActionResult> Details(int id)
        {
            mpm.ProductDetails = (from p in context.Products where p.ProductID == id select p).FirstOrDefault();

            mpm.CategoryName = (from p in context.Products
                                join c in context.Categories
                                on p.CategoryID equals c.CategoryID
                                where p.ProductID == id
                                select c.CategoryName).FirstOrDefault();

            mpm.BrandName = (from p in context.Products
                             join s in context.Suppliers
                             on p.SupplierID equals s.SupplierID
                             where p.ProductID == id
                             select s.BrandName).FirstOrDefault();

            mpm.RealetedProducts = context.Products.Where(p => p.Related == mpm.ProductDetails!.Related && p.ProductID != id).ToList();

            return View(mpm);
        }

        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            try
            {

                if (userRepository.loginEmailControl(user) == false)
                {
                    bool answer = userRepository.AddUser(user);

                    if (answer)
                    {
                        TempData["Message"] = "Kaydedildi.";
                        return RedirectToAction("Login");
                    }
                    TempData["Message"] = "Hata.Tekrar deneyiniz.";
                }
                else
                {
                    TempData["Message"] = "Bu Email Zaten mevcut.Baþka Deneyiniz.";
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Exception: {ex.Message}");
                TempData["Message"] = "Bir hata oluþtu. Lütfen tekrar deneyiniz.";
            }
            return View();

        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("Admin");
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(User user)
        {

            string answer = userRepository.MemberControl(user);
            if (answer == "error")
            {
                HttpContext.Session.SetString("Mesaj", "Email/Þifre yanlýþ girildi");
                TempData["Message"] = "Email/Þifre yanlýþ girildi";
                return View();
            }
            else if (answer == "admin")
            {
                HttpContext.Session.SetString("Email", answer);
                HttpContext.Session.SetString("Admin", answer);
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                HttpContext.Session.SetString("Email", answer);
                return RedirectToAction("Index");
            }
        }





    }
}
