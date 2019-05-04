using Microsoft.EntityFrameworkCore;
using Starter.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Starter.Service
{
    public class BaseDbContext : DbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Books> Books { get; set; }
    }
}
