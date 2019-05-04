using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Starter.Common.Extension;
using Starter.Entity.Domain;
using Starter.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Service
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private Guid TransGuid { get; set; } = Guid.NewGuid();
        public DbContext dbContext { private get; set; }

        public void RegisterNew<TEntity>(TEntity entity)
             where TEntity : EntityCore
        {
            ThrowExceptionIfEntityIsInvalid(entity);
            dbContext.Set<TEntity>().Add(entity);
        }
        public async Task RegisterNewAsync<TEntity>(TEntity entity)
        where TEntity : EntityCore
        {
            ThrowExceptionIfEntityIsInvalid(entity);
            await dbContext.Set<TEntity>().AddAsync(entity);
        }
        public void RegisterNewRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : EntityCore
        {
            if (entities == null || entities.Count() == 0)
                throw new ArgumentNullException(nameof(entities));
            foreach (var entity in entities)
            {
                ThrowExceptionIfEntityIsInvalid(entity);
            }
            dbContext.Set<TEntity>().AddRange(entities);
        }

        public async Task RegisterNewRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : EntityCore
        {
            if (entities == null || entities.Count() == 0)
                throw new ArgumentNullException(nameof(entities));
            foreach (var entity in entities)
            {
                ThrowExceptionIfEntityIsInvalid(entity);
            }
            await dbContext.Set<TEntity>().AddRangeAsync(entities);
        }
        public void RegisterDirty<TEntity>(TEntity entity)
            where TEntity : EntityCore
        {
            ThrowExceptionIfEntityIsInvalid(entity);
            var entry = dbContext.Entry(entity);
            entry.State = EntityState.Modified;
            entity.LastChangeTime = DateTime.Now;
        }


        public void RegisterClean<TEntity>(TEntity entity)
            where TEntity : EntityCore
        {
            var entry = dbContext.Entry(entity);
            entry.State = EntityState.Unchanged;
        }

        public void RegisterDeleted<TEntity>(TEntity entity)
            where TEntity : EntityCore
        {
            dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<bool> CommitAsync()
        {
            var isSuccess = await dbContext.SaveChangesAsync() > 0;
            return isSuccess;
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public bool Commit()
        {
            var isSuccess = dbContext.SaveChanges() > 0;
            return isSuccess;
        }

        private string PropertyValuesToModel(PropertyValues values)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{");
            foreach (var propertyName in values.Properties)
            {
                stringBuilder.Append(string.Format("\"{0}\": \"{1}\",", propertyName.Name, values[propertyName.Name]));
            }
            var rstring = stringBuilder.ToString().TrimEnd(',') + "}";
            return rstring;
        }

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
    }
}
