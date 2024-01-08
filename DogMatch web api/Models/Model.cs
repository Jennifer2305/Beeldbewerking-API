using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Numerics;
using DogMatch_web_api.Entities;
using Microsoft.Extensions.Configuration;

namespace Models
{
    public class DataContext : DbContext
    {
        public DbSet<Afgewezen> afgewezen {get;set;}
        public DbSet<Hond> hond {get;set;}
        public DbSet<Hond_Profiel> hondprofiel {get;set;}
        public DbSet<Match> match {get;set;}
        public DbSet<Profiel> profiel {get;set;}
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfiguration configuration = builder.Build();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("WebApiDatabase"));
        }

         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Afgewezen>().HasKey(t => new { t.Hond1Id, t.Hond2Id });
            modelBuilder.Entity<Afgewezen>().HasOne(ma => ma.Hond1).WithMany(m => m.AfgewezenHond1).HasForeignKey(ma => ma.Hond1Id);
            modelBuilder.Entity<Afgewezen>().HasOne(ma => ma.Hond2).WithMany(m => m.AfgewezenHond2).HasForeignKey(ma => ma.Hond2Id);
            modelBuilder.Entity<Afgewezen>().Property(p => p.Hond1Id).HasColumnType("bigint");
            modelBuilder.Entity<Afgewezen>().Property(p => p.Hond2Id).HasColumnType("bigint");

            modelBuilder.Entity<Match>().HasKey(t => new { t.Hond1Id, t.Hond2Id });
            modelBuilder.Entity<Match>().HasOne(ma => ma.Hond1).WithMany(m => m.MatchedHond1).HasForeignKey(ma => ma.Hond1Id);
            modelBuilder.Entity<Match>().HasOne(ma => ma.Hond2).WithMany(m => m.MatchedHond2).HasForeignKey(ma => ma.Hond2Id);
            modelBuilder.Entity<Match>().Property(p => p.Hond1Id).HasColumnType("bigint");
            modelBuilder.Entity<Match>().Property(p => p.Hond2Id).HasColumnType("bigint");

            modelBuilder.Entity<Hond_Profiel>().HasKey(t => new { t.HondId, t.ProfielId });
            modelBuilder.Entity<Hond_Profiel>().HasOne(ma => ma.Hond).WithOne(m => m.Profiel).HasForeignKey<Hond_Profiel>(m => m.HondId);
            modelBuilder.Entity<Hond_Profiel>().HasOne(ma => ma.Profiel).WithOne(m => m.Hond).HasForeignKey<Hond_Profiel>(m => m.ProfielId);
            modelBuilder.Entity<Hond_Profiel>().Property(p => p.HondId).HasColumnType("bigint");
            modelBuilder.Entity<Hond_Profiel>().Property(p => p.ProfielId).HasColumnType("bigint");

            modelBuilder.Entity<Hond>().Property(p => p.Id).HasColumnType("bigint");
            modelBuilder.Entity<Hond>().Property(p => p.Naam).HasColumnType("character varying (200)");
            modelBuilder.Entity<Hond>().Property(p => p.Geslacht).HasColumnType("character varying (4)");
            modelBuilder.Entity<Hond>().Property(p => p.Foto).HasColumnType("character varying (200)");

            modelBuilder.Entity<Profiel>().Property(p => p.Id).HasColumnType("bigint");
            modelBuilder.Entity<Profiel>().Property(p => p.Beschrijving).HasColumnType("character varying (200)");
            modelBuilder.Entity<Profiel>().Property(p => p.Voorkeur).HasColumnType("character varying (5)");            
        }
    }
}