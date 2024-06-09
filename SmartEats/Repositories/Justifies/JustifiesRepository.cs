using SmartEats.DataBase;
using SmartEats.Models.Justifies;

namespace SmartEats.Repositories.Justifies
{
    public class JustifiesRepository : BaseRepository<ApplicationDBContext, Justify>, IJustifiesRepository
    {
        public JustifiesRepository(ApplicationDBContext context) : base(context)
        {
        }
    }
}
