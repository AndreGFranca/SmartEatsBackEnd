using SmartEats.Models.Companies;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartEats.Models.Confirms
{
    public class Confirm
    {
        [Key]
        public int Id { get; set; }
        public DateOnly DataConfirmacao { get; set; }
        public TimeOnly HoraDeAlmoco { get; set; }
        public bool Compareceu { get; set; } = false;
        public DateTime? HorarioComparecimento { get; set; }
        public bool Confirmou { get; set; }
        public int IdEmpresa { get; set; }

        [ForeignKey("IdEmpresa")]
        public virtual Company Empresa { get; set; }

    }
}
