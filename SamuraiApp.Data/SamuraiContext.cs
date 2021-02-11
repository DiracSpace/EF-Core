using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SamuraiApp.Domain;
using System;
using System.Diagnostics;

namespace SamuraiApp.Data
{
    public class SamuraiContext : DbContext
    {
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=SamuraiAppData")
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name },
                        LogLevel.Information)
                .EnableSensitiveDataLogging();
        }

        /// <summary>
        /// 
        /// Adds mapping to the Many-To-Many
        /// relationship between Battles and Samurai
        /// where instead of inner join we define a class
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Samurai>()
                .HasMany(samurai => samurai.Battles)
                .WithMany(battle => battle.Samurais)
                .UsingEntity<BattleSamurai>
                 (war => war.HasOne<Battle>().WithMany(),
                  war => war.HasOne<Samurai>().WithMany())
                .Property(war => war.DateJoined)
                .HasDefaultValueSql("getDate()");
        }
    }
}
