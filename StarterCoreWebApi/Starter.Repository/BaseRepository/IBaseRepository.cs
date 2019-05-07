using Starter.Entity.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Starter.Repository
{
    public interface IBaseRepository<TEntity>
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        void Insert(TEntity entity);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="express"></param>
        void Delete(Expression<Func<TEntity, bool>> express);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="express"></param>
        /// <returns></returns>
        void DeleteForge(Expression<Func<TEntity, bool>> express);

        /// <summary>
        /// 查询对象
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> express);
    }
}
