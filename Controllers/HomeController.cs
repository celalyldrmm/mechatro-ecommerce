using mechatro_ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        public IActionResult CategoryPage(int id)
        {
            List<Product> products = productRepository.ProductSelectWithCategoryID(id);
            return View(products);
        }

        public IActionResult Cart()
        {
            List<OrderRepository> sepet;

            if (HttpContext.Request.Query["scid"].ToString() != "")

            {

                string? scid = HttpContext.Request.Query["scid"];
                orderRepository.MyCart = Request.Cookies["sepetim"];
                orderRepository.DeleteFromMyCart(scid);
                var cookieOptions = new CookieOptions();
                Response.Cookies.Append("sepetim", orderRepository.MyCart, cookieOptions);
                cookieOptions.Expires = DateTime.Now.AddDays(1);
                TempData["Message"] = "Ürün Sepetinizden Silindi";
                sepet = orderRepository.SelectMyCart();
                ViewBag.Sepetim = sepet;
                ViewBag.sepet_tablo_detay = sepet;
            }
            else
            {
                var cookie = Request.Cookies["sepetim"];
                if (cookie == null)
                {
                    orderRepository.MyCart = "";
                    sepet = orderRepository.SelectMyCart();
                    ViewBag.Sepetim = sepet;
                    ViewBag.sepet_tablo_detay = sepet;
                }
                else
                {
                    var cookieOptions = new CookieOptions();
                    orderRepository.MyCart = Request.Cookies["sepetim"];
                    sepet = orderRepository.SelectMyCart();
                    ViewBag.Sepetim = sepet;
                    ViewBag.sepet_tablo_detay = sepet;
                }

            }
            mpm.FeaturedProducts = productRepository.ProductSelect("Featured", mainpageCount, "Featured", 0);
            return View(mpm);
        }

        public IActionResult CartProcess(int id)
        {

            orderRepository.ProductID = id;
            orderRepository.Quantity = 1;

            var cookieOptions = new CookieOptions();

            var cookie = Request.Cookies["sepetim"];

            if (cookie == null)
            {

                cookieOptions = new CookieOptions();
                cookieOptions.Expires = DateTime.Now.AddDays(1);
                cookieOptions.Path = "/";
                orderRepository.MyCart = "";
                orderRepository.AddToMyCart(id.ToString());
                Response.Cookies.Append("sepetim", orderRepository.MyCart, cookieOptions);
                TempData["Message"] = "Ürün Sepetinize Eklendi";
            }
            else
            {
                orderRepository.MyCart = cookie;
                if (orderRepository.AddToMyCart(id.ToString()) == false)
                {
                    HttpContext.Response.Cookies.Append("sepetim", orderRepository.MyCart, cookieOptions);
                    cookieOptions.Expires = DateTime.Now.AddDays(1);
                    TempData["Message"] = "Ürün Sepetinize Eklendi";
                }
                else
                {

                    TempData["Message"] = "Bu Ürün Zaten Sepetinizde Var";

                }
            }

            string url = Request.Headers["Referer"].ToString();
            return Redirect(url);
        }
        public IActionResult Order()
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                User? usr = userRepository.SelectMemberInfo(HttpContext.Session.GetString("Email"));
                return View(usr);
            }
            else
            {

                return RedirectToAction("Login");
            }
        }
        public IActionResult MyOrders()
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                var mainpagemodel = new MainPageModel();
                mainpagemodel.MyOrders = orderRepository.SelectMyOrders(HttpContext.Session.GetString("Email").ToString());
                mainpagemodel.RealetedProducts = productRepository.ProductSelect("RealetedProducts", mainpageCount, "RealetedProducts", 0);

                return View(mainpagemodel);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }


        [HttpPost]
        public IActionResult Order(IFormCollection frm)
        {
            string txt_individual = Request.Form["txt_individual"];
            string txt_corporate = Request.Form["txt_corporate"];

            if (txt_individual != null)
            {
                //bireysel fatura
                //digital planet
                // WebServiceController.tckimlik_vergi_no = txt_individual;
                orderRepository.tckimlik_vergi_no = txt_individual;
                orderRepository.EfaturaCreate();
            }
            else
            {
                //kurumsal fatura
                //  WebServiceController.tckimlik_vergi_no = txt_corporate;
                orderRepository.tckimlik_vergi_no = txt_corporate;
                orderRepository.EfaturaCreate();
            }

            string kredikartno = Request.Form["kredikartno"];//1.yol = formun içindeki textboxalanýnn deðerini 
            string kredikartay = frm["kredikartay"];//2.yol=formun içindeki textboxalanýnn deðerini 
            string kredikartyil = frm["kredikartyil"];
            string kredikartcvs = frm["kredikartcvs"];

            return RedirectToAction("backref");




        }
        public IActionResult ConfirmPage()
        {
            ViewBag.OrderGroupGUID = OrderGroupGUID;
            mpm.RealetedProducts = productRepository.ProductSelect("RealetedProducts", mainpageCount, "RealetedProducts", 0);
            return View(mpm);
        }
        public IActionResult backref()

        {
            ConfirmOrder();
            return RedirectToAction("ConfirmPage");
        }

        public static string OrderGroupGUID = "";

        public IActionResult ConfirmOrder()

        {
            var cookieOptions = new CookieOptions();
            var cookie = Request.Cookies["sepetim"];

            if (cookie != null)

            {

                orderRepository.MyCart = cookie;
                OrderGroupGUID = orderRepository.OrderCreate(HttpContext.Session.GetString("Email").ToString());
                cookieOptions.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Delete("sepetim");

            }

            return RedirectToAction("ConfirmPage");

        }

        public PartialViewResult gettingProducts(string id)
        {
            id = id.ToUpper(new System.Globalization.CultureInfo("tr-TR"));
            List<SP_Search> ulist = ProductRepository.gettingSearchProducts(id);
            string json = JsonConvert.SerializeObject(ulist);
            var response = JsonConvert.DeserializeObject<List<Search>>(json);

            return PartialView(response);
        }

    }
}
