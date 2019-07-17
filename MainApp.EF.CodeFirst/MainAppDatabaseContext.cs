using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace MainApp.EF.CodeFirst
{
    public class MainAppDatabaseContext : DbContext
    {
        //private IConfiguration Configuration { get; set; }

        public MainAppDatabaseContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            //Configuration = configuration;
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(Configuration.GetConnectionString("CFConnectionString"));
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CachedWordsEntity>(entity =>
            {
                entity.Property(e => e.Word).HasMaxLength(255);

                entity.HasOne(d => d.Anagram)
                    .WithMany(p => p.CachedWords)
                    .HasForeignKey(d => d.AnagramId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_CachedWords_Words");
            });

            modelBuilder.Entity<UserLogEntity>(entity =>
            {
                entity.Property(e => e.SearchTime).HasColumnType("datetime");

                entity.Property(e => e.UserIp).HasMaxLength(15);

                entity.Property(e => e.Word).HasMaxLength(255);
            });

            modelBuilder.Entity<WordsEntity>(entity =>
            {
                entity.Property(e => e.Word).HasMaxLength(255);
            });

            modelBuilder.Entity<NumOfAllowedSearchesEntity>(entity =>
            {
                entity.Property(e => e.UserIp).HasMaxLength(15);
            });
        }

        public DbSet<WordsEntity> Words { get; set; }
        public DbSet<CachedWordsEntity> CachedWords { get; set; }
        public DbSet<UserLogEntity> UserLog { get; set; }
        public DbSet<NumOfAllowedSearchesEntity> NumOfAllowedSearches { get; set; }
    }
}
