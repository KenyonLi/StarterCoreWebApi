using Microsoft.EntityFrameworkCore;
using Starter.Entity;
using Starter.Service.DBContextMange;
using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.Service
{
    public class ReadDbContext : DbContext
    {
        public ReadDbContext(DbContextOptions<ReadDbContext> options) : base(options)
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
            //modelBuilder.Entity<Department>().ToTable("Departments").Property(p => p.RowVersion).IsRowVersion();
            #endregion
        }
        public DbSet<User> Users { get; set; }

    }
}
