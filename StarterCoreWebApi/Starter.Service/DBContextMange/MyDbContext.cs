﻿using Microsoft.EntityFrameworkCore;
using Starter.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.Service
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }

    }
}
