using SmartEats.Models.Users;
using System.ComponentModel.DataAnnotations;

namespace SmartEats.Models.Companies
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public string CNPJ { get; set; }
        public virtual IList<User>? Workers { get; set; }


    }
}
