
using System.Net.Mail;
using System.Net;
using XAct;
using System.Text;

namespace mechatro_ecommerce.Models
{
  
    public class OrderRepository : IOrderRepository
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string? MyCart { get; set; } 
        public decimal UnitPrice { get; set; }
        public string? ProductName { get; set; }
        public int Kdv { get; set; }
        public string? PhotoPath { get; set; }
        public string? tckimlik_vergi_no { get; set; }

        MechatroContext context = new MechatroContext();
        public bool AddToMyCart(string id)
        {
            bool exists = false;
            if (MyCart == "")
            {
                MyCart = id + "=1";
            }
            else
            {
                
                string[] MyCartArray = MyCart.Split('&'); 
                for (int i = 0; i < MyCartArray.Length; i++)
                {
                    string[] MyCartArrayLoop = MyCartArray[i].Split("=");
                    if (MyCartArrayLoop[0] == id)
                    {
                        exists = true; 
                    }
                }
                if (exists == false)
                {
                    MyCart = MyCart + "&" + id.ToString() + "=1";
                }
            }
            return exists;
        }

        public void DeleteFromMyCart(string id)
        {
            string[] MyCartArray = MyCart.Split('&');
            string NewMyCart = "";
            int count = 1; 

            for (int i = 0; i < MyCartArray.Length; i++)
            {
                string[] MyCartArrayLoop = MyCartArray[i].Split('=');
                
                string MyCartID = MyCartArrayLoop[0];
                if (MyCartID != id)
                {
                    
                    if (count == 1)
                    {
                        
                        NewMyCart = MyCartArrayLoop[0] + "=" + MyCartArrayLoop[1];
                        count++;
                    }
                    else
                    {
                        NewMyCart += "&" + MyCartArrayLoop[0] + "=" + MyCartArrayLoop[1];
                    }
                }
            } 
            MyCart = NewMyCart;
        }

        public void EfaturaCreate()
        {
            //digital planet xml dosyası
        }

        public string OrderCreate(string Email)
        {
            List<OrderRepository> sipList = SelectMyCart();
            
            string OrderGroupGUID = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace(".", "");
            DateTime OrderDate = DateTime.Now; ;

            foreach (var item in sipList)
            {
                Order order = new Order();
                order.OrderDate = OrderDate;
                order.OrderGroupGUID = OrderGroupGUID;
                order.UserID = context.Users.FirstOrDefault(u => u.Email == Email).UserID;
                order.ProductID = item.ProductID;
                order.Quantity = item.Quantity;
                context.Orders.Add(order);
                context.SaveChanges();
            }
            return OrderGroupGUID;
        }

        public List<OrderRepository> SelectMyCart()
        {
            List<OrderRepository> list = new List<OrderRepository>();
            string[] MyCartArray = MyCart.Split('&');

            if (MyCartArray[0] != "")
            {
                for (int i = 0; i < MyCartArray.Length; i++)
                {
                    string[] MyCartArrayLoop = MyCartArray[i].Split('=');
                    int MyCartID = Convert.ToInt32(MyCartArrayLoop[0]);

                    Product? prd = context.Products.FirstOrDefault(p => p.ProductID == MyCartID);

                    OrderRepository ord = new OrderRepository();
                    ord.ProductID = prd.ProductID;
                    ord.Quantity = Convert.ToInt32(MyCartArrayLoop[1]);
                    ord.UnitPrice = prd.UnitPrice;
                    ord.ProductName = prd.ProductName;
                    ord.PhotoPath = prd.PhotoPath;
                    ord.Kdv = prd.KDV;
                    list.Add(ord);
                }
            }
            return list;
        }

        public List<vw_MyOrders> SelectMyOrders(string Email)
        {
            int UserID = context.Users.FirstOrDefault(u => u.Email == Email).UserID;

            List<vw_MyOrders> myOrders = context.vw_MyOrders.Where(o => o.UserID == UserID).ToList();

            return myOrders;
        }

        public void Send_Email(string OrderGroupGUID)
        {
            using (MechatroContext context = new MechatroContext())
            {
                Order order = context.Orders.FirstOrDefault(o => o.OrderGroupGUID == OrderGroupGUID);
                User user = context.Users.FirstOrDefault(u => u.UserID == order.UserID);

                string mail = "gonderen email buraya";
                string _mail = user.Email;
                string subject = "";
                string content = "";

                content = "Sayın " + user.NameSurname + "," + DateTime.Now + " tarihinde " + OrderGroupGUID + " nolu siparişiniz alınmıştır.";

                subject = "Sayın " + user.NameSurname + " siparişiniz alınmıştır.";

                string host = "smtp";
                int port = 587;
                string login = "mailserver a baglanılan login buraya";
                string password = "mailserver a baglanılan şifre buraya";

                MailMessage e_posta = new MailMessage();
                e_posta.From = new MailAddress(mail, "info@mechatro.com, bilgi"); 
                e_posta.To.Add(_mail);
                e_posta.Subject = subject;
                e_posta.IsBodyHtml = true;
                e_posta.Body = content;

                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new NetworkCredential(login, password);
                smtp.Port = port;
                smtp.Host = host;

                try
                {
                    smtp.Send(e_posta);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void Send_Sms(string OrderGroupGUID)
        {
            using (MechatroContext context = new MechatroContext())
            {
                string ss = "";
                ss += "<?xml version='1.0' encoding='UTF-8' >";
                ss += "<mainbody>";
                ss += "<header>";
                ss += "<company dil='TR'>mechatro(üye oldugunuzda size verilen şirket ismi)</company>";
                ss += "<usercode>0850 size verilen user kod burada yazılacak</usercode>";
                ss += "<password>NetGsm123. size verilen şifre burada yazılacak</password>";
                ss += "<startdate></startdate>";
                ss += "<stopdate></stopdate>";
                ss += "<type>n:n</type>";
                ss += "<msgheader>başlık buraya</msgeader>";
                ss += "</header>";
                ss += "<body>";

                Order order = context.Orders.FirstOrDefault(o => o.OrderGroupGUID == OrderGroupGUID);
                User user = context.Users.FirstOrDefault(u => u.UserID == order.UserID);
                //Sayın xxxx, 15 04 202x tarihinde 5042023194420 nolu siparişiniz alınmıştır.
                string content = "Sayın " + user.NameSurname + "," + DateTime.Now + " tarihinde " + OrderGroupGUID + " nolu siparişiniz alınmıştır.";

                ss += "<mp><msg><![CDATA[" + content + "]]></msg><no>90" + user.Telephone + "</no></mp>";
                ss += "</body>";
                ss += "</mainbody>";

                string answer = XMLPOST("https://api.netgsm.com/tr/xmlbulkhttppost.asp", ss);
                if (answer != "-1")
                {
                    //sms gitti
                }
                else
                {
                    //sms gitmedi
                }
            }
        }

        public string XMLPOST(string url, string xmlData)
        {
            try
            {
                WebClient wUpload = new WebClient();
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest; 
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                Byte[] bPostArray = Encoding.UTF8.GetBytes(xmlData);
                Byte[] bResonse = wUpload.UploadData(url, "POST", bPostArray);

                Char[] sReturnsChars = Encoding.UTF8.GetChars(bResonse);

                string sWebPage = new string(sReturnsChars);
                return sWebPage;
            }
            catch (Exception)
            {
                return "-1";
            }
        }

    }
}
