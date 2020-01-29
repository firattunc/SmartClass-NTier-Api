using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Konu:IEntity
    {
        public int Id { get; set; }
        public string KonuAdi { get; set; }
        //public int DersId { get; set; }
        [JsonIgnore]
        public virtual ICollection<AltBasliklar> AltBasliklar { get; set; }
        //public virtual Ders Ders { get; set; }
        [JsonIgnore]
        public virtual ICollection<Soru> Sorus { get; set; }
        public Konu()
        {
            AltBasliklar = new HashSet<AltBasliklar>();
            Sorus = new HashSet<Soru>();
        }
      
    }
    public class KonuEntityConfiguration : IEntityTypeConfiguration<Konu>
    {
        public void Configure(EntityTypeBuilder<Konu> builder)
        {
            //builder.HasOne<Ders>(navigationExpression: s => s.Ders)
            //    .WithMany(navigationExpression: g => g.Konus)
            //    .HasForeignKey(s => s.DersId)
            //    .OnDelete(DeleteBehavior.Restrict)
            //    .IsRequired();
        }
    }
}
