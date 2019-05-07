using Microsoft.EntityFrameworkCore;
using Starter.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Service.Infrastructure
{
    /// <summary>
    ///  工作单元
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext writeDbContext { private get; set; }
        public DbContext readDbContext { private get; set; }

        public bool Commit()
        {
            throw new NotImplementedException();
        }

        public Task<bool> CommitAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> ExecuteFromSql<TEntity>(string sql)
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
