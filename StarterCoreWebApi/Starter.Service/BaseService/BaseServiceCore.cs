using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Starter.Entity;
using Starter.Entity.Domain;
using Starter.Repository;
using Starter.Repository.Infrastructure;
using Starter.Service.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Service
{
    public class BaseServiceCore<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected DbSet<TEntity> _writeEntities;
        protected DbSet<TEntity> _readEntities;
        protected UnitOfWork _unitofwork;
        public BaseServiceCore(DbContext writeContext, DbContext readContext)
        {
            _unitofwork = new UnitOfWork();

            _writeEntities = writeContext.Set<TEntity>();
            _readEntities = readContext.Set<TEntity>();

            _unitofwork.writeDbContext = writeContext;
            _unitofwork.readDbContext = readContext;
        }
        public void Delete(Expression<Func<TEntity, bool>> express)
        {
            throw new NotImplementedException();
        }

        public void DeleteForge(Expression<Func<TEntity, bool>> express)
        {
            throw new NotImplementedException();
        }
        public void Insert(TEntity entity)
        {
            _writeEntities.Add(entity);
        }
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> express)
        {
            return _readEntities.Where(express).AsTracking();
        }
        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
