using SmartEats.Enums.Users;
using SmartEats.Models.Companies;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartEats.DTOs.Users
{
    public class CreateUserDTO
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public string UserName { get; set; }
        public int Id_Company { get; set; }
        public TypeUser TypeUser { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "As senhas devem ser iguais")]
        public string RePassword { get; set; }
    }
}
