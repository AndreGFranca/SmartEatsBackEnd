using SmartEats.DataBase;
using SmartEats.Models.Menus;

namespace SmartEats.Repositories.Menus
{
    public class MenusRepository : BaseRepository<ApplicationDBContext, Menu>, IMenusRepository
    {
        public MenusRepository(ApplicationDBContext context) : base(context)
        {
        }
    }
}
