using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartEats.Models.Menus
{
    public class PlateDay
    {
        [Key]
        public int Id { get; set; }
        public string Prato { get; set; }
        public DateOnly CardapioDate { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey("CardapioDate,CompanyId")]
        public virtual Menu Cardapio {get;set;}
    }
}
