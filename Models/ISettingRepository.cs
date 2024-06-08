namespace mechatro_ecommerce.Models
{
    public interface ISettingRepository
    {
        Task<List<Setting>> SettingSelect();
        bool SettingInsert(Setting settings);
        Task<Setting> SettingDetails(int? id);
        bool SettingUpdate(Setting settings);
        bool SettingDelete(int id);
    }
}
