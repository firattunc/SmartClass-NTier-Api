using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Cevap:IEntity
    {
        public int Id { get; set; }
        public DateTime Tarih { get; set; }
        public string IsTrue { get; set; }
        public int SoruId { get; set; }
        public int OgrenciId { get; set; }
        public int TestSonucId { get; set; }
        public virtual Soru Soru { get; set; }
        public virtual Ogrenci Ogrenci { get; set; }
        public virtual TestSonuc TestSonuc { get; set; }
       
    }
    public class CevapEntityConfiguration : IEntityTypeConfiguration<Cevap>
    {
        public void Configure(EntityTypeBuilder<Cevap> builder)
        {
            builder.HasOne<Soru>(navigationExpression: s => s.Soru)
                .WithMany(navigationExpression: g => g.Cevaps)
                .HasForeignKey(s => s.SoruId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            builder.HasOne<Ogrenci>(navigationExpression: s => s.Ogrenci)
                .WithMany(navigationExpression: g => g.Cevaps)
                .HasForeignKey(s => s.OgrenciId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            builder.HasOne<TestSonuc>(navigationExpression: s => s.TestSonuc)
                .WithMany(navigationExpression: g => g.Cevaps)
                .HasForeignKey(s => s.TestSonucId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
