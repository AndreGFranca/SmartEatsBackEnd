using System.ComponentModel.DataAnnotations;

namespace SmartEats.DTOs.Justifies
{
    public class CreateJustifyDTO
    {
        [Required]
        public string Justificativa { get; set; }
        [Required]
        public int IdConfirmacao { get; set; }
        [Required]
        public string IdFuncionario { get; set; }
        [Required]
        public int IdEmpresa { get; set; }
    }
}
