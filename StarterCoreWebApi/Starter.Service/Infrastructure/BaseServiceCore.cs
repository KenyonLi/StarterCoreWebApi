using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Starter.Entity;
using Starter.Entity.Domain;
using Starter.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Service
{
    public class BaseServiceCore<T> : IBaseRepository<T> where T : EntityCore
    {
        protected readonly DbSet<T> _entities;
        protected readonly BaseDbContext _dbContext;
        public BaseServiceCore(BaseDbContext context)
        {
            _entities = context.Set<T>();
            _dbContext = context;
        }
        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="entity"></param>
        void IBaseRepository<T>.Delete(T entity)
        {
            _dbContext.Remove(entity);
        }
        #region -批量删除（伪删除）-
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteForge(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            entity.IsDelete = true;
            entity.LastChangeTime = DateTime.Now;
            ThrowExceptionIfEntityIsInvalid(entity);
            var entry = _dbContext.Entry(entity);
            entry.State = EntityState.Modified;
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="express"></param>
        public void DeleteForge(Expression<Func<T, bool>> express)
        {
            var entity = Query(false).Where(express).FirstOrDefault();
            if (entity != null)
                DeleteForge(entity);
        }
        #endregion

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void Deleted<TEntity>(TEntity entity)
           where TEntity : EntityCore
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="isFilter"></param>
        /// <returns></returns>
        public IQueryable<T> Query(bool isFilter = true)
        {
            return isFilter ? _entities.AsTracking().Where(p => !p.IsDelete) : _entities.AsTracking();
        }

        /// <summary>
        /// 回滚
        /// </summary>
        public void Rollback()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        public bool Commit()
        {
            var isSuccess = _dbContext.SaveChanges() > 0;
            return isSuccess;
        }

        #region -添加、批量添加、事务提交 异步-

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddAsync<TEntity>(TEntity entity)
   where TEntity : EntityCore
        {
            ThrowExceptionIfEntityIsInvalid(entity);
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        /// <summary>
        /// 批量添加 异步
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : EntityCore
        {
            if (entities == null || entities.Count() == 0)
                throw new ArgumentNullException(nameof(entities));
            foreach (var entity in entities)
            {
                ThrowExceptionIfEntityIsInvalid(entity);
            }
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
        }
        /// <summary>
        /// 提交事务 异步
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CommitAsync()
        {
            var isSuccess = await _dbContext.SaveChangesAsync() > 0;
            return isSuccess;
        }
        #endregion

        #region -验证-
        /// <summary>
        /// 验证数据的正确性
        /// </summary>
        /// <param name="entity">实体</param>
        private void ThrowExceptionIfEntityIsInvalid<TEntity>(TEntity entity) where TEntity : EntityCore
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            if (entity.GetBrokenRules().Any())
            {
                var brokenRules = new StringBuilder();
                brokenRules.AppendLine("数据验证不通过，错误信息：");
                foreach (var businessRule in entity.GetBrokenRules())
                {
                    brokenRules.AppendLine(businessRule.Rule);
                }
                throw new Exception(brokenRules.ToString());
            }
        }

        public bool ExecuteSqlCommand(string sql)
        {
            _dbContext.Database.ExecuteSqlCommand(sql);
            return Commit();
        }

        public async Task<bool> ExecuteSqlCommandAsync(string sql)
        {
            await _dbContext.Database.ExecuteSqlCommandAsync(sql);
            return await CommitAsync();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void Edit<TEntity>(TEntity entity) where TEntity : EntityCore
        {
            ThrowExceptionIfEntityIsInvalid(entity);
            var entry = _dbContext.Entry(entity);
            entry.State = EntityState.Modified;
            entity.LastChangeTime = DateTime.Now;
        }

        public IEnumerable<TEntity> ExecuteFromSql<TEntity>(string sql) where TEntity : EntityCore
        {
            var queryList = _dbContext.Set<TEntity>().FromSql(sql);
            return queryList;
        }

        #endregion
    }
}
