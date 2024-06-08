using mechatro_ecommerce.Models;
using Microsoft.AspNetCore.Mvc;
using XAct;

namespace mechatro_ecommerce.ViewComponents
{
    public class Address:ViewComponent
    {
        MechatroContext context = new MechatroContext();
        public string Invoke()
        {
            string? address = context.Settings.FirstOrDefault(s => s.SettingID == 1).Address;
            return $"{address}";
        }
    }
}
