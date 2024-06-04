using System.ComponentModel.DataAnnotations;

namespace SmartEats.DTOs.Users
{
    public class PasswordChangeDTO
    {

        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        [Compare("RePassword", ErrorMessage ="A senhas não são iguais")]
        [MinLength(8, ErrorMessage = "Deve Conter no minimo 8 caracteres")]
        public string NewPassword { get; set; }
        [Required]
        [MinLength(8, ErrorMessage = "Deve Conter no minimo 8 caracteres")]
        public string RePassword { get; set; }
    }
}
