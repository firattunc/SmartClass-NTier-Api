using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class GenelIstatistik:IEntity
    {
        public int Id { get; set; }
        public int DogruSayisi { get; set; }
        public int OgrenciId { get; set; }
        public int AltBaslikId { get; set; }
        public virtual Ogrenci Ogrenci { get; set; }
        public virtual AltBasliklar AltBaslik { get; set; }
       
    }
    public class GenelIstatistikEntityConfiguration : IEntityTypeConfiguration<GenelIstatistik>
    {
        public void Configure(EntityTypeBuilder<GenelIstatistik> builder)
        {
            builder.HasOne<Ogrenci>(navigationExpression: s => s.Ogrenci)
                .WithMany(navigationExpression: g => g.GenelIstatistiks)
                .HasForeignKey(s => s.OgrenciId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            builder.HasOne<AltBasliklar>(navigationExpression: s => s.AltBaslik)
               .WithMany(navigationExpression: g => g.GenelIstatistiks)
               .HasForeignKey(s => s.AltBaslikId)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired();
        }
    }
}
