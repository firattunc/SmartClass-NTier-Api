using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Soru:IEntity
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        public string Cevap { get; set; }
        public int DersId { get; set; }        
        public int KonuId { get; set; }
        [JsonIgnore]
        public virtual Ders Ders { get; set; }
        [JsonIgnore]
        public virtual Konu Konu { get; set; }
        [JsonIgnore]
        public virtual ICollection<SoruAltBaslik> SoruAltBasliks { get; set; }
        [JsonIgnore]
        public virtual ICollection<Cevap> Cevaps { get; set; }
        public Soru()
        {
            SoruAltBasliks = new HashSet<SoruAltBaslik>();
            Cevaps = new HashSet<Cevap>();
        }


    }
    public class SoruEntityConfiguration : IEntityTypeConfiguration<Soru>
    {
        public void Configure(EntityTypeBuilder<Soru> builder)
        {
            builder.HasOne<Ders>(navigationExpression: s => s.Ders)
                .WithMany(navigationExpression: g => g.Sorus)
                .HasForeignKey(s => s.DersId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            builder.HasOne<Konu>(navigationExpression: s => s.Konu)
                .WithMany(navigationExpression: g => g.Sorus)
                .HasForeignKey(s => s.KonuId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
