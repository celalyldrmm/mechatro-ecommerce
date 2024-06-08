using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace mechatro_ecommerce.Models
{
    public class Setting
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [DisplayName("ID")]
        public int SettingID { get; set; }

        [DisplayName("Telefon Numarası")]
        public string? Telephone { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [DisplayName("Adres")]
        public string? Address { get; set; }

        [DisplayName("Ana Sayfada Gösterilen Ürün Sayısı")]
        public int MainpageCount { get; set; }

        [DisplayName("Alt Sayfada Gösterilen Ürün Sayısı")]
        public int SubpageCount { get; set; }
    }
}
