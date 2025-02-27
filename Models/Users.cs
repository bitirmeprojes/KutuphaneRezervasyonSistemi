using System.ComponentModel.DataAnnotations;

namespace KTRS.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "İsim adı gerekli.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyisim adı gerekli.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı gerekli.")]
        public string username { get; set; }

        [Required(ErrorMessage = "Email adresi gerekli.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre gerekli.")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(ErrorMessage = "Şifre tekrar gerekli.")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string confirmPassword { get; set; }


        public string Role { get; set; } = "S"; 


    }
}
