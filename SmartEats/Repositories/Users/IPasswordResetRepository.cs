using SmartEats.DataBase;
using SmartEats.Models.Menus;
using SmartEats.Models.Users;
using SmartEats.Repositories.Menus;

namespace SmartEats.Repositories.Users
{
    public interface IPasswordResetRepository: IBaseRepository<ApplicationDBContext, PasswordResetCode>
    {
    }
}
