using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace mechatro_ecommerce.Models
{
    public class Status
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

		[DisplayName("ID")]
		public int StatusID { get; set; }
        [Required]
		[DisplayName("Durum Adı")]
		public string? StatusName { get; set; }
		[DisplayName("Aktif/Pasif")]
		public bool IsActive { get; set; }
    }
}
