using SmartEats.DataBase;
using SmartEats.Models.Users;

namespace SmartEats.Repositories.Users
{
    public class UsersRepository : BaseRepository<ApplicationDBContext, User>, IUsersRepository
    {
        public UsersRepository(ApplicationDBContext context) : base(context)
        {
        }
    }
}
