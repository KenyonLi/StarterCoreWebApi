using Microsoft.EntityFrameworkCore;
using Starter.Entity.Domain;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Common.Extension
{
    /// <summary>
    /// IQueryable对象扩展
    /// </summary>
    public static class IQueryableExtension
    {
        public static IQueryable<TSource> HasWhere<TSource>(this IQueryable<TSource> query, object target,
            Expression<Func<TSource, bool>> whExpression)
        {
            if (target != null)
            {
                query = query.Where(whExpression);
            }
            return query;
        }
        public static IQueryable<TSource> HasWhere<TSource>(this IQueryable<TSource> query, object target,
            Expression<Func<TSource, int, bool>> whExpression)
        {
            if (target != null)
            {
                query = query.Where(whExpression);
            }
            return query;
        }
        /// <summary>
        /// 读取列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="isOrderBy"></param>
        /// <returns></returns>
        public static Page<T> ToPage<T>(this IQueryable<T> query,
            int pageIndex,
            int pageSize,
            bool isOrderBy = false)
        {
            var page = new Page<T>();
            var totalItems = query.Count();
            var totalPages = (totalItems % pageSize) == 0 ? (totalItems / pageSize) : (totalItems / pageSize) + 1;
            page.CurrentPage = pageIndex;
            page.ItemsPerPage = pageSize;
            page.TotalItems = totalItems;
            page.TotalPages = totalPages;
            page.Items = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return page;
        }


        /// <summary>
        /// 读取列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="isOrderBy"></param>
        /// <returns></returns>
        public static async Task<Page<T>> ToPageAsync<T>(this IQueryable<T> query,
            int pageIndex,
            int pageSize,
            bool isOrderBy = false)
        {
            var page = new Page<T>();
            var totalItems = await query.CountAsync();
            var totalPages = totalItems != 0 ? (totalItems % pageSize) == 0 ? (totalItems / pageSize) : (totalItems / pageSize) + 1 : 0;
            page.CurrentPage = pageIndex;
            page.ItemsPerPage = pageSize;
            page.TotalItems = totalItems;
            page.TotalPages = totalPages;
            page.Items = totalItems == 0 ? null : await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return page;
        }
    }
}
