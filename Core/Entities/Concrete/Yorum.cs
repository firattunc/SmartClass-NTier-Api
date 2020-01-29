using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Yorum:IEntity
    {
        public int Id { get; set; }
        public string Metin { get; set; }
        public DateTime Tarih { get; set; }
        public int OgrenciId { get; set; }
        public int TestSonucId { get; set; }
        [JsonIgnore]
        public virtual Ogrenci Ogrenci { get; set; }
        [JsonIgnore]
        public virtual TestSonuc  TestSonuc{ get; set; }
       

    }
    public class YorumEntityConfiguration : IEntityTypeConfiguration<Yorum>
    {
        public void Configure(EntityTypeBuilder<Yorum> builder)
        {
            builder.HasOne<Ogrenci>(navigationExpression: s => s.Ogrenci)
                .WithMany(navigationExpression: g => g.Yorums)
                .HasForeignKey(s => s.OgrenciId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            builder.HasOne<TestSonuc>(navigationExpression: s => s.TestSonuc)
                .WithMany(navigationExpression: g => g.Yorums)
                .HasForeignKey(s => s.TestSonucId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
