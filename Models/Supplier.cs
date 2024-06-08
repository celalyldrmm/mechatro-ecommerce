using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace mechatro_ecommerce.Models
{
    public class Supplier
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[DisplayName("ID")]
		public int SupplierID { get; set; }

		[StringLength(100)]
		[Required(ErrorMessage = "Tedarikçi adı zorunlu alan")]

		[DisplayName("Marka Adı")]
		public string? BrandName { get; set; }

		[DisplayName("Marka Resmi")] 
		public string? PhotoPath { get; set; }

		[DisplayName("Aktif/Pasif")] 
		public bool IsActive { get; set; }
    }
}
