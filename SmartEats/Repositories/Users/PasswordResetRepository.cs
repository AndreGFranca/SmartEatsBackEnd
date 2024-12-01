using SmartEats.DataBase;
using SmartEats.Models.Users;

namespace SmartEats.Repositories.Users
{
    public class PasswordResetRepository : BaseRepository<ApplicationDBContext, PasswordResetCode>, IPasswordResetRepository
    {
        public PasswordResetRepository(ApplicationDBContext context) : base(context)
        {
        }
    }
}
