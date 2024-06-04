using SmartEats.DataBase;
using SmartEats.Models.Confirms;

namespace SmartEats.Repositories.Confirms
{
    public class ConfirmsRepository : BaseRepository<ApplicationDBContext, Confirm>, IConfirmsRepository
    {
        public ConfirmsRepository(ApplicationDBContext context) : base(context)
        {
        }
    }
}
