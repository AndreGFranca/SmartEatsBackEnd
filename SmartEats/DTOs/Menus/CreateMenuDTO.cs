using SmartEats.Models.Companies;
using SmartEats.Models.Menus;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartEats.DTOs.Menus
{
    public class CreateMenuDTO
    {
        [Required]
        public DateOnly Data { get; set; }
        [Required]
        public int IdEmpresa { get; set; }
        [Required]
        public IList<PlateDay> PlatesDay { get; set; }
    }
}
