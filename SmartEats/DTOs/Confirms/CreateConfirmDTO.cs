using SmartEats.Models.Companies;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartEats.DTOs.Confirms
{
    public class CreateConfirmDTO
    {
        [Required]
        public DateOnly DataConfirmacao { get; set; }
        [Required]
        public TimeOnly HoraDeAlmoco { get; set; }
        public bool Compareceu { get; set; } = false;        
        public bool Confirmou { get; set; } = true;
        [Required]
        public string IdFuncionario { get; set; }
        [Required]
        public int IdEmpresa { get; set; }

    }
}
