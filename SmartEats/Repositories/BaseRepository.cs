
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SmartEats.Models;

namespace SmartEats.Repositories
{
    public abstract class BaseRepository<TBanco, TEntity> : IDisposable, IBaseRepository<TBanco, TEntity> where TEntity : class where TBanco : DbContext
    {
        protected readonly TBanco _context;
        protected BaseRepository(TBanco context)
        {
            _context = context;
        }
        public async virtual Task Add(TEntity entity)
        {
            using (var context = _context)
            {
                context.Set<TEntity>().Add(entity);
                await context.SaveChangesAsync();
            }
                
        }

        public async virtual Task AddRange(IList<TEntity> entity)
        {
            using (var context = _context)
            {
                context.Set<TEntity>().AddRange(entity);
                await context.SaveChangesAsync();
            }
        }

        public async virtual Task Delete(TEntity entity)
        {
            using (var context = _context)
            {
                context.Set<TEntity>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public virtual IQueryable<TEntity> Search()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public async virtual Task Update(TEntity entity)
        {
            using (var context = _context)
            {
                context.Set<TEntity>().Update(entity);
                await _context.SaveChangesAsync();
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
