using mechatro_ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using XAct;

namespace mechatro_ecommerce.ViewComponents
{
    public class Email:ViewComponent
    {
        MechatroContext context=new MechatroContext();
        public string Invoke()
        {
            string? email = context.Settings.FirstOrDefault(s => s.SettingID == 1).Email;
            return $"{email}";
        }
    }
}
