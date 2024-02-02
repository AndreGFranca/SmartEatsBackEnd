using SmartEats.Models.Users;

namespace SmartEats.Models.Companies
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IList<User>? Workers { get; set; }


    }
}
