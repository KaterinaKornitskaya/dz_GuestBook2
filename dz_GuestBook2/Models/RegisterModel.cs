using System.ComponentModel.DataAnnotations;

namespace dz_GuestBook2.Models
{
    public class RegisterModel
    {
        [Required]
        [Display(Name = "Имя")]
        public string? Name { get; set; }

        [Required]
        [Display(Name = "Логин")]
        public string? Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Подтверждение пароля")]
        public string? PasswordConfirm { get; set; }
    }
}
