﻿using SmartEats.Models.Companies;
using SmartEats.Models.Confirms;
using SmartEats.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartEats.Models.Justifies
{
    public class Justify
    {
        [Key]
        public int Id { get; set; }
        public string Justificativa { get; set; }
        public bool? Aprovado { get; set; }
        public int IdConfirmacao { get; set; }
        public string? MotivoRecusa { get; set; }

        public string IdFuncionario { get; set; }
        public string? IdAprovador { get; set; }
        public int IdEmpresa { get; set; }
        [ForeignKey("IdFuncionario")]
        public virtual User Funcionario { get; set; }
        [ForeignKey("IdAprovador")]
        public virtual User? Aprovador { get; set; }
        [ForeignKey("IdEmpresa")]
        public virtual Company Empresa { get; set; }
        [ForeignKey("IdConfirmacao")]
        public virtual Confirm Confirmacao { get; set; }
    }
}
