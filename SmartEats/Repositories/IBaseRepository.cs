using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace SmartEats.Repositories
{
    public interface IBaseRepository<TBanco, TEntity>: IDisposable where TBanco : DbContext
    {
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task AddRange(IList<TEntity> entity);
        Task Delete(TEntity entity);
        IQueryable<TEntity> Search();        
    }
}
