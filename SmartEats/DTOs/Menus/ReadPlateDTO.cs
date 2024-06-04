using SmartEats.Models.Menus;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartEats.DTOs.Menus
{
    public class ReadPlateDto
    {
        public int Id { get; set; }
        public string Prato { get; set; }
        public DateOnly CardapioDate { get; set; }
        public int CompanyId { get; set; }
    }
}
