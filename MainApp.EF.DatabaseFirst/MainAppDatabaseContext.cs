using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MainApp.EF.DatabaseFirst
{
    public partial class MainAppDatabaseContext : DbContext
    {
        public MainAppDatabaseContext()
        {
        }

        public MainAppDatabaseContext(DbContextOptions<MainAppDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CachedWords> CachedWords { get; set; }
        public virtual DbSet<UserLog> UserLog { get; set; }
        public virtual DbSet<Words> Words { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=LT-LIT-SC-0009;Initial Catalog=MainAppDatabase;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CachedWords>(entity =>
            {
                entity.Property(e => e.Word).HasMaxLength(255);

                entity.HasOne(d => d.Anagram)
                    .WithMany(p => p.CachedWords)
                    .HasForeignKey(d => d.AnagramId)
                    .HasConstraintName("FK_CachedWords_Words");
            });

            modelBuilder.Entity<UserLog>(entity =>
            {
                entity.Property(e => e.SearchTime).HasColumnType("datetime");

                entity.Property(e => e.UserIp).HasMaxLength(15);

                entity.Property(e => e.Word).HasMaxLength(255);
            });

            modelBuilder.Entity<Words>(entity =>
            {
                entity.Property(e => e.Word).HasMaxLength(255);
            });
        }
    }
}
