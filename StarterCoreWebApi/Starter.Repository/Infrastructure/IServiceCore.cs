using System;
using System.Linq;
using System.Linq.Expressions;

namespace Starter.Repository
{
    public interface IServiceCore<T>
    {
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void DeleteForge(T entity);
        /// <summary>
        /// 查询对象
        /// </summary>
        /// <param name="isFilter">是否过滤已执行伪删除的数据，默认为true:过滤</param>
        /// <returns></returns>
        IQueryable<T> Query(bool isFilter = true);

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="express"></param>
        /// <returns></returns>
        void DeleteForge(Expression<Func<T, bool>> express);
    }
}
