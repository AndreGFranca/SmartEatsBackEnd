using SmartEats.Models.Menus;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartEats.DTOs.Menus
{
    public class CreatePlateDTO
    {
        [Required]
        public string Prato { get; set; }
        [Required]
        public DateOnly CardapioDate { get; set; }
        [Required]
        public int CompanyId { get; set; }
    }
}
