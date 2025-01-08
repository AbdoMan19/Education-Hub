using MVC_Task.Repository;
using System.Data;

namespace MVC_Task.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> SaveChanges();

        IGenericRepository<TEntity> Repository<TEntity>()
            where TEntity : class;

        Task BeginTransactionAsync(IsolationLevel isolationLevel);

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();
    }
}
