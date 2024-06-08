using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace mechatro_ecommerce.Models
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [DisplayName("ID")]
        public int CategoryID { get; set; }
        public int ParentID { get; set; }
        private string? _CategoryName { get; set; }
        [DisplayName("Kategori Adı")] 
        public string? CategoryName
        {
            get { return _CategoryName; }
            set { _CategoryName = value?.ToUpper(); }
        }

        [DisplayName("Aktif Mi?")]
        public bool IsActive { get; set; }
    }
}
