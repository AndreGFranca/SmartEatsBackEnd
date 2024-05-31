using SmartEats.Enums.Users;
using System.ComponentModel.DataAnnotations;

namespace SmartEats.DTOs.Users
{
    public class EditUserDTO
    {        
        [Required(ErrorMessage = "Campo nome é obrigatório")]
        [MinLength(1, ErrorMessage = "Campo nome requer no minimo 1 caracter")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Campo cpf é obrigatório")]
        [Length(14,14,ErrorMessage = "Campo cpf requer no minimo 14 caracteres")]
        public string CPF { get; set; }
        [EmailAddress(ErrorMessage ="Deve ser um e-mail valido")]
        public string UserName { get; set; }
    }
}
