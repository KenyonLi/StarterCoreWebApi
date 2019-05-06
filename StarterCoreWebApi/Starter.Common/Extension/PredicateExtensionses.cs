using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Starter.Common.Extension
{
    //调用方式
    //Expression<Func<OneToOneUser, bool>> where = PredicateExtensionses.True<OneToOneUser>();
    //where=where.And(p => p.UserID == Guid.Empty);
    /// <summary>
    /// ParameterExpression入口
    /// </summary>
    public static class PredicateExtensionses
    {
        public static Expression<Func<T, bool>> True<T>() { return f => true; }

        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> exp_left, Expression<Func<T, bool>> exp_right)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            var left = parameterReplacer.Replace(exp_left.Body);
            var right = parameterReplacer.Replace(exp_right.Body);
            var body = Expression.And(left, right);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> exp_left, Expression<Func<T, bool>> exp_right)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            var left = parameterReplacer.Replace(exp_left.Body);
            var right = parameterReplacer.Replace(exp_right.Body);
            var body = Expression.Or(left, right);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }

        /// <summary>
        /// Distinc 扩展方法（自定义）
        /// 使用方法：var query = people.DistinctBy(p => new { p.Id, p.Name });
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {

            HashSet<TKey> seenKeys = new HashSet<TKey>();

            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }

    /// <summary>
    /// 统一ParameterExpression
    /// </summary>
    internal class ParameterReplacer : ExpressionVisitor
    {
        public ParameterReplacer(ParameterExpression paramExpr)
        {
            this.ParameterExpression = paramExpr;
        }

        public ParameterExpression ParameterExpression { get; private set; }

        public Expression Replace(Expression expr)
        {
            return this.Visit(expr);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            return this.ParameterExpression;
        }
    }
}
