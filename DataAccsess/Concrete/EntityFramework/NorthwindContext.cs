using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Concrete;
using static Core.Entities.Concrete.AltBasliklar;
using static Core.Entities.Concrete.Konu;
using static Core.Entities.Concrete.Cevap;
using static Core.Entities.Concrete.GenelIstatistik;
using static Core.Entities.Concrete.Ilce;
using static Core.Entities.Concrete.Soru;
using static Core.Entities.Concrete.TestSonuc;
using static Core.Entities.Concrete.Yorum;

namespace DataAccess.Concrete.EntityFramework
{
    public class NorthwindContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            optionsBuilder.UseSqlServer(connectionString: @"Server=DESKTOP-GBR32OJ;Database=smartClassCoreDB;Trusted_Connection=true");
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<Konu> Konus { get; set; }
        public DbSet<AltBasliklar> AltBasliklars { get; set; }
        public DbSet<Cevap> Cevaps { get; set; }
        public DbSet<Ders> Ders { get; set; }
        public DbSet<GenelIstatistik> GenelIstatistiks { get; set; }
        public DbSet<Il> Ils { get; set; }
        public DbSet<Ilce> Ilces { get; set; }
        public DbSet<Ogrenci> Ogrencis { get; set; }
        public DbSet<Ogretmen> Ogretmens { get; set; }
        public DbSet<Okul> Okuls { get; set; }
        public DbSet<Soru> Sorus { get; set; }
        public DbSet<SoruAltBaslik> SoruAltBasliks { get; set; }
        public DbSet<TestSonuc> TestSonucs { get; set; }
        public DbSet<Yorum> Yorums { get; set; }
        public DbSet<OgrenciOgretmeni> OgrenciOgretmenis { get; set; }
         
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserOperationClaimEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AltBasliklarEntityConfiguration());
            modelBuilder.ApplyConfiguration(new KonuEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CevapEntityConfiguration());
            modelBuilder.ApplyConfiguration(new GenelIstatistikEntityConfiguration());
            modelBuilder.ApplyConfiguration(new IlceEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SoruEntityConfiguration());
            modelBuilder.ApplyConfiguration(new TestSonucEntityConfiguration());
            modelBuilder.ApplyConfiguration(new YorumEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OgrenciOgretmeniEntityConfiguration());

        }
    }
}
