using SmartEats.DTOs.Confirms;
using SmartEats.DTOs.Users;
using SmartEats.Models.Companies;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartEats.DTOs.Justifies
{
    public class ConfirmJustifyDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public bool Aprovado { get; set; }        
        public string? MotivoRecusa { get; set; }
        [Required]
        public string IdAprovador { get; set; }
        [Required]
        public int IdEmpresa { get; set; }

    }
}
