using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;

namespace MainApp.EF.CodeFirst
{
    public class MainAppDatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.AppSettings["CFConnectionString"]);
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

        public DbSet<Words> Words { get; set; }
        public DbSet<CachedWords> CachedWords { get; set; }
        public DbSet<UserLog> UserLog { get; set; }
    }
}
