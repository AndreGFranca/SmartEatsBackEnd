using SmartEats.DataBase;
using SmartEats.Models.Users;

namespace SmartEats.Repositories.Users
{
    public interface IUsersRepository: IBaseRepository<ApplicationDBContext, User>
    {
    }
}
