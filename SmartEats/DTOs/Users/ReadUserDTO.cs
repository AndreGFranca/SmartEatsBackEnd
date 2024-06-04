using SmartEats.Enums.Users;
using SmartEats.Models.Companies;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartEats.DTOs.Users
{
    public class ReadUserDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string UserName { get; set; }
        public TypeUser TypeUser { get; set; }
        public bool Ativo { get; set; }
        public int Id_Company { get; set; }
        [ForeignKey("Id_Company")]
        public virtual Company Company { get; set; }

    }
}
