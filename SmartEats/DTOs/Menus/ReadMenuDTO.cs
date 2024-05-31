using SmartEats.Models.Menus;
using System.ComponentModel.DataAnnotations;

namespace SmartEats.DTOs.Menus
{
    public class ReadMenuDTO
    {
        public DateOnly Data { get; set; }
        public int IdEmpresa { get; set; }
        public IList<PlateDay> PlatesDay { get; set; }
    }
}
