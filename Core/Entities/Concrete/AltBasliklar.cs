using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class AltBasliklar:IEntity
    {
        public int Id { get; set; }
        public string AltBaslikAdi { get; set; }
        public int KonuId { get; set; }
        [JsonIgnore]
        public virtual Konu Konu { get; set; }
        [JsonIgnore]
        public virtual ICollection<GenelIstatistik> GenelIstatistiks { get; set; }
        public AltBasliklar()
        {
            GenelIstatistiks = new HashSet<GenelIstatistik>();
        }
        public class AltBasliklarEntityConfiguration : IEntityTypeConfiguration<AltBasliklar>
        {
            public void Configure(EntityTypeBuilder<AltBasliklar> builder)
            {
                builder.HasOne<Konu>(navigationExpression: s => s.Konu)
                    .WithMany(navigationExpression: g => g.AltBasliklar)
                    .HasForeignKey(s => s.KonuId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            }
        }
    }
}
