using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class OgrenciOgretmeni:IEntity
    {
        public int Id { get; set; }
        public int OgrenciId { get; set; }
        public int OgretmenId { get; set; }        
        public virtual Ogrenci Ogrenci { get; set; }
        public virtual Ogretmen Ogretmen { get; set; }
    }
    public class OgrenciOgretmeniEntityConfiguration : IEntityTypeConfiguration<OgrenciOgretmeni>
    {
        public void Configure(EntityTypeBuilder<OgrenciOgretmeni> builder)
        {
            builder.HasOne<Ogrenci>(navigationExpression: s => s.Ogrenci)
                .WithMany(navigationExpression: g => g.OgrenciOgretmenis)
                .HasForeignKey(s => s.OgrenciId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            builder.HasOne<Ogretmen>(navigationExpression: s => s.Ogretmen)
                .WithMany(navigationExpression: g => g.OgrenciOgretmenis)
                .HasForeignKey(s => s.OgretmenId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
