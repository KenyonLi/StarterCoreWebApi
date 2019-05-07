using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Repository.Infrastructure
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 提交事务 
        /// </summary>
        /// <returns></returns>
        bool Commit();
        /// <summary>
        /// 提交事务 异步
        /// </summary>
        /// <returns></returns>
        Task<bool> CommitAsync();
        /// <summary>
        /// 回滚
        /// </summary>
        void Rollback();
        /// <summary>
        /// 查询复杂 sql语句
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        IEnumerable<TEntity> ExecuteFromSql<TEntity>(string sql);
    }
}
