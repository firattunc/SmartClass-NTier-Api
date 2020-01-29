using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class TestSonuc:IEntity
    {
        public int Id { get; set; }
        public DateTime Tarih { get; set; }
        public DateTime BitisTarih { get; set; }
        public int DogruSayisi { get; set; }
        public int OgrenciId { get; set; }
        [JsonIgnore]
        public virtual Ogrenci Ogrenci { get; set; }
        [JsonIgnore]
        public virtual ICollection<Cevap> Cevaps { get; set; }
        [JsonIgnore]
        public virtual ICollection<Yorum> Yorums { get; set; }

        public TestSonuc()
        {
            Cevaps = new HashSet<Cevap>();
            Yorums = new HashSet<Yorum>();
        }
       
    }
    public class TestSonucEntityConfiguration : IEntityTypeConfiguration<TestSonuc>
    {
        public void Configure(EntityTypeBuilder<TestSonuc> builder)
        {
            builder.HasOne<Ogrenci>(navigationExpression: s => s.Ogrenci)
                .WithMany(navigationExpression: g => g.TestSonucs)
                .HasForeignKey(s => s.OgrenciId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
