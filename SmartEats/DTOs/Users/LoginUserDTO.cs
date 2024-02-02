using SmartEats.Enums.Users;
using SmartEats.Models.Companies;
using System.ComponentModel.DataAnnotations;

namespace SmartEats.DTOs.Users
{
    public class LoginUserDTO
    {
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
    }
}
