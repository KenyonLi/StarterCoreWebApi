using Microsoft.EntityFrameworkCore;
using Starter.Entity.Domain;
using Starter.Repository;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Starter.Service
{
    public class ServiceCore<T> : IServiceCore<T> where T : EntityCore
    {
        protected readonly DbSet<T> _entities;
        protected readonly UnitOfWork _unitofwork;

        public ServiceCore(DbContext context)
        {
            _unitofwork = new UnitOfWork();
            _entities = context.Set<T>();
            _unitofwork.dbContext = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void DeleteForge(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.IsDelete = true;
            entity.LastChangeTime = DateTime.Now;
            _unitofwork.RegisterDirty(entity);
        }


        public IQueryable<T> Query(bool isFilter = true)
        {
            return isFilter ? _entities.AsTracking().Where(p => !p.IsDelete) : _entities.AsTracking();
        }

        public void DeleteForge(Expression<Func<T, bool>> express)
        {
            var entity = Query(false).Where(express).FirstOrDefault();
            if (entity != null)
                DeleteForge(entity);
        }

    }
}
