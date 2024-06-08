
using Microsoft.EntityFrameworkCore;

namespace mechatro_ecommerce.Models
{
    public class SettingRepository : ISettingRepository
    {
        MechatroContext context = new MechatroContext();
        public bool SettingDelete(int id)
        {
            try
            {
                using (MechatroContext context = new MechatroContext())
                {
                    Setting settings = context.Settings.FirstOrDefault(c => c.SettingID == id);
                   
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Setting> SettingDetails(int? id)
        {
            Setting? settings = await context.Settings.FirstOrDefaultAsync(S => S.SettingID == id);
            return settings;
        }

        public bool SettingInsert(Setting settings)
        {
            try
            {
                using (MechatroContext context = new MechatroContext())
                {
                    context.Add(settings);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<List<Setting>> SettingSelect()
        {
            List<Setting> settings = await context.Settings.ToListAsync();
            return settings;
        }

        public bool SettingUpdate(Setting settings)
        {
            try
            {
                using (MechatroContext context = new MechatroContext())
                {
                    context.Update(settings);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
