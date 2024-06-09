using SmartEats.DTOs.Confirms;
using SmartEats.DTOs.Users;
using SmartEats.Models.Companies;
using SmartEats.Models.Confirms;
using SmartEats.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartEats.DTOs.Justifies
{
    public class ReadJustifyDTO
    {
        public int Id { get; set; }
        public string Justificativa { get; set; }
        public bool? Aprovado { get; set; }
        public int IdConfirmacao { get; set; }
        public string? MotivoRecusa { get; set; }

        public string IdFuncionario { get; set; }
        public string? IdAprovador { get; set; }
        public int IdEmpresa { get; set; }
        [ForeignKey("IdFuncionario")]
        public virtual ReadUserDTO Funcionario { get; set; }
        [ForeignKey("IdAprovador")]
        public virtual ReadUserDTO? Aprovador { get; set; }
        [ForeignKey("IdEmpresa")]
        public virtual Company Empresa { get; set; }
        [ForeignKey("IdConfirmacao")]
        public virtual ReadConfirmDTO Confirmacao { get; set; }
    }
}
