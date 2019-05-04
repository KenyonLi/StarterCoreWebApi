using System.Collections.Generic;
using System.Text;

namespace Starter.Repository
{
    public interface IRepository<TEntity, TPrimaryKey>
    {
        bool Insert(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TPrimaryKey entity);
    }
}
