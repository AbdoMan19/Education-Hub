using System.Linq.Expressions;

namespace MVC_Task.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll(List<string> include);
        public Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(int id);

        TEntity Add(TEntity myObject);

        Task<IEnumerable<TEntity>> GetData(Expression<Func<TEntity, bool>> predicate, List<string>? include =null);

        Task<TEntity> Update(TEntity entity);

        Task<TEntity?> Delete(int id);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, bool disableTracking = true);




    }
}
