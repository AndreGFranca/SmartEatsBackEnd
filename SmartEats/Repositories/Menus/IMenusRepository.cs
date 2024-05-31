using Microsoft.EntityFrameworkCore;
using SmartEats.DataBase;
using SmartEats.Models.Menus;

namespace SmartEats.Repositories.Menus
{
    public interface IMenusRepository : IBaseRepository<ApplicationDBContext, Menu>
    {
    }
}
