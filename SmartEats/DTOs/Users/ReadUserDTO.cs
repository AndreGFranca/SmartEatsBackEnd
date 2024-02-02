using SmartEats.Models.Companies;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartEats.DTOs.Users
{
    public class ReadUserDTO
    {
        public string Name { get; set; }
        public int Id_Company { get; set; }
        [ForeignKey("Id_Company")]
        public virtual Company Company { get; set; }

    }
}
