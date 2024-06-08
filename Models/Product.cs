using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mechatro_ecommerce.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID")]
        public int ProductID { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Ürün adı Zorunlu alan")]
        [DisplayName("Ürün Adı")]
        public string? ProductName { get; set; }
        [DisplayName("Fiyat")]
        public decimal UnitPrice { get; set; }

        [DisplayName("Kategori")] 
        public int CategoryID { get; set; }

        [DisplayName("Marka")]
        public int SupplierID { get; set; }
        [DisplayName("Stok")]

        public int Stock { get; set; }
        [DisplayName("İndirim")]

        public int Discount { get; set; }

        [DisplayName("Statüs")]
        public int StatusID { get; set; }

        public DateTime AddDate { get; set; }

        [DisplayName("Anahtar Kelimeler")]
        public string? Keywords { get; set; } 

        private int _Kdv { get; set; }
        public int KDV
        {
            get { return _Kdv; }
            set { _Kdv = Math.Abs(value); }
        }

        [DisplayName("Buna Bakanlar")]
        public int Related { get; set; } 
        [DisplayName("Notlar")]
        public string? Notes { get; set; }
        [DisplayName("Resim")]
        public string? PhotoPath { get; set; }
        [DisplayName("Aktif/Pasif")] 
		public bool IsActive { get; set; }

    }
}
