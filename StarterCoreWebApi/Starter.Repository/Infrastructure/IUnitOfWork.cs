using Starter.Entity.Domain;
using System.Threading.Tasks;

namespace Starter.Repository
{
    public interface IUnitOfWork
    {
        void RegisterNew<TEntity>(TEntity entity)
       where TEntity : EntityCore;

        void RegisterDirty<TEntity>(TEntity entity)
            where TEntity : EntityCore;

        void RegisterClean<TEntity>(TEntity entity)
            where TEntity : EntityCore;

        void RegisterDeleted<TEntity>(TEntity entity)
            where TEntity : EntityCore;

        Task<bool> CommitAsync();

        bool Commit();

        void Rollback();
    }
}
