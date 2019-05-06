using Starter.Entity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Starter.Repository
{
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void DeleteForge(T entity);
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="express"></param>
        /// <returns></returns>
        void DeleteForge(Expression<Func<T, bool>> express);

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Delete(T entity);
        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        void Edit<TEntity>(TEntity entity) where TEntity : EntityCore;

        /// <summary>
        /// 查询对象
        /// </summary>
        /// <param name="isFilter">是否过滤已执行伪删除的数据，默认为true:过滤</param>
        /// <returns></returns>
        IQueryable<T> Query(bool isFilter = true);


        /// <summary>
        /// 提交事务 异步
        /// </summary>
        /// <returns></returns>
        Task<bool> CommitAsync();
        /// <summary>
        /// 提交事务 
        /// </summary>
        /// <returns></returns>
        bool Commit();
        /// <summary>
        /// 回滚
        /// </summary>
        void Rollback();
        /// <summary>
        /// 执行sql 语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        bool ExecuteSqlCommand(string sql);

        Task<bool> ExecuteSqlCommandAsync(string sql);
        /// <summary>
        /// 查询复杂 sql语句
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        IEnumerable<TEntity> ExecuteFromSql<TEntity>(string sql) where TEntity : EntityCore;
    }
}
