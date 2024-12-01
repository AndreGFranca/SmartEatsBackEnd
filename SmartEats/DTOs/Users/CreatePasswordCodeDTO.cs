namespace SmartEats.DTOs.Users
{
    public class CreatePasswordCodeDTO
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public DateTime Expiration { get; set; }
    }
}
