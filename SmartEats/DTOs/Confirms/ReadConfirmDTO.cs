using SmartEats.DTOs.Users;
using SmartEats.Models.Companies;
using SmartEats.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartEats.DTOs.Confirms
{
    public class ReadConfirmDTO
    {
        public int Id { get; set; }
        public DateOnly DataConfirmacao { get; set; }
        public TimeOnly HoraDeAlmoco { get; set; }
        public bool Compareceu { get; set; }
        public bool Confirmou { get; set; }
        public int IdEmpresa { get; set; }
        public string IdFuncionario { get; set; }
        public virtual ReadUserDTO Funcionario { get; set; }
    }
}
