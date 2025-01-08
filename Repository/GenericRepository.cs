using Microsoft.EntityFrameworkCore;
using MVC_Task.DB;
using System.Linq.Expressions;

namespace MVC_Task.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationContext _context;
        DbSet<TEntity> _dbSet;
        public GenericRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public TEntity Add(TEntity entity)
        {
            return _dbSet.Add(entity).Entity;
            
        }
        public async Task<TEntity?> Delete(int id)
        {
            var entity = await GetById(id);
            if(entity == null)return null;
            return _dbSet.Remove(entity).Entity;
        }
        public async Task<TEntity?> GetById(int id)
        {
            var result = await _dbSet.FindAsync(id);
            return result;
        }
        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, bool disableTracking = true)
        {
            return disableTracking
                ? _context.Set<TEntity>().Where(predicate).AsNoTracking()
                : _context.Set<TEntity>().Where(predicate);
        }
        public async Task<IEnumerable<TEntity>> GetAll(List<string> Include)
        {
            var _dbSetQueryable = _context.Set<TEntity>().AsQueryable();
            foreach (var item in Include)
                _dbSetQueryable = _dbSetQueryable.Include(item);

            var result = _dbSetQueryable.ToListAsync();
            return await result;
        }
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var result = _dbSet.ToListAsync();
            return await result;
        }
        public async Task<TEntity> Update(TEntity entity)
        {
            return _dbSet.Update(entity).Entity;
        }
        public async Task<IEnumerable<TEntity>> GetData(Expression<Func<TEntity, bool>> predicate, List<string>? Include = null)
        {
            var _dbSetQueryable = _context.Set<TEntity>().AsQueryable();
            if(Include != null)
            {
                foreach (var item in Include)
                { 
                    _dbSetQueryable = _dbSetQueryable.Include(item);
                }

            }
            var result = await _dbSetQueryable.Where(predicate).ToListAsync();
            return result;
        }

    }
}
