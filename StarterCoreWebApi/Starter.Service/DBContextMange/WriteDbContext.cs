using Microsoft.EntityFrameworkCore;
using Starter.Common;
using Starter.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.Service
{
    /// <summary>
    /// 写
    /// </summary>
    public class WriteDbContext : BaseDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(AppSetting.WriteDB);
        }
    }
    /// <summary>
    /// 读
    /// </summary>
    public class ReadDbContext : BaseDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(AppSetting.ReadDb);
        }
    }
}
