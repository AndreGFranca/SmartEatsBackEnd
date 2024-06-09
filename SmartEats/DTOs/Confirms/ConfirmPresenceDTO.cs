using System.ComponentModel.DataAnnotations;

namespace SmartEats.DTOs.Confirms
{
    public class ConfirmPresenceDTO
    {
        [Required]
        public bool Compareceu { get; set; }
        public DateTime? HorarioComparecimento { get; set; }
    }
}
