using mechatro_project.Models;
using Microsoft.AspNetCore.Mvc;
using XAct;

namespace mechatro_project.ViewComponents
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
