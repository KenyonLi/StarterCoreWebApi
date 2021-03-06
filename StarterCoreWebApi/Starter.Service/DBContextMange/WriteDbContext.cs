﻿using Microsoft.EntityFrameworkCore;
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
    public class WriteDbContext : DbContext
    {
        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Authorities
            //modelBuilder.Entity<Role>().ToTable("Roles").Property(p => p.RowVersion).IsRowVersion();
            //modelBuilder.Entity<RoleGroup>().ToTable("RoleGroups").Property(p => p.RowVersion).IsRowVersion();
            //modelBuilder.Entity<RoleInRoleGroup>().ToTable("RoleInRoleGroups").Property(p => p.RowVersion).IsRowVersion();
            //modelBuilder.Entity<User>().ToTable("Users").Property(p => p.RowVersion).IsRowVersion();
            //modelBuilder.Entity<UserMenu>().ToTable("UserMenus").Property(p => p.RowVersion).IsRowVersion();
            //modelBuilder.Entity<UserAction>().ToTable("UserActions").Property(p => p.RowVersion).IsRowVersion();
            //modelBuilder.Entity<UserMenuOrActionInRole>().ToTable("UserMenuOrActionInRoles").Property(p => p.RowVersion).IsRowVersion();
            //modelBuilder.Entity<UserInRoleGroup>().ToTable("UserInRoleGroups").Property(p => p.RowVersion).IsRowVersion();
            //modelBuilder.Entity<User>().ToTable("User").Property(p => p.Id).IsRowVersion();
            #endregion
        }

        public DbSet<User> Users { get; set; }
    }
}
