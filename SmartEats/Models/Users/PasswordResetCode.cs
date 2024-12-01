using System.ComponentModel.DataAnnotations;

namespace SmartEats.Models.Users
{
    public class PasswordResetCode
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public DateTime Expiration { get; set; }
    }
}
