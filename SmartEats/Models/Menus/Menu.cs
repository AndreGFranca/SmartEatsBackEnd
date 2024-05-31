using SmartEats.Models.Companies;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartEats.Models.Menus
{
    public class Menu
    {
        [Key, Column(Order = 0)]
        public DateOnly Data { get; set; }
        [Key, Column(Order = 1)]
        public int IdEmpresa { get; set; }

        [ForeignKey("IdEmpresa")]
        public virtual Company Empresa { get; set; }
        public virtual IList<PlateDay>? PlatesDay { get; set; }
    }
}
