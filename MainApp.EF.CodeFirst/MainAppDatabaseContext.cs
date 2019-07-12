using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainApp.EF.CodeFirst
{
    class MainAppDatabaseContext : DbContext
    {
        public MainAppDatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Words> Words { get; set; }
        public DbSet<CachedWords> CachedWords { get; set; }
        public DbSet<UserLog> UserLog { get; set; }
    }
}
