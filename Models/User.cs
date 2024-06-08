using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace mechatro_ecommerce.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int UserID { get; set; }

        [StringLength(150)]
        [Required]
        public string? NameSurname { get; set; }

        [StringLength(150)]
        [Required(ErrorMessage = " Email GİRİNİZ")]
        [EmailAddress]
        public string? Email { get; set; }
        [StringLength(50)]

        [Required(ErrorMessage = "Kullanıcı Adı GİRİNİZ")]
        public string? UserName { get; set; }

        [StringLength(150)]
        [Required(ErrorMessage = "Şifre GİRİNİZ")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public string? Telephone { get; set; }
        public string? InvoicesAddres { get; set; } 
        public bool IsAdmin { get; set; } 

        public bool IsActive { get; set; }
    }
}
