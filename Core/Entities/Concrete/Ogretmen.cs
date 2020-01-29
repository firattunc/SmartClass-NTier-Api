using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Ogretmen:IEntity
    {
        public int Id { get; set; }
        public int OkulId { get; set; }
        public int UserId { get; set; }
        public virtual Okul Okul { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OgrenciOgretmeni> OgrenciOgretmenis { get; set; }
        public Ogretmen()
        {
            OgrenciOgretmenis = new HashSet<OgrenciOgretmeni>();
        }
    }

    public class OgretmenrEntityConfiguration : IEntityTypeConfiguration<Ogretmen>
    {
        public void Configure(EntityTypeBuilder<Ogretmen> builder)
        {
            builder.HasOne<Okul>(navigationExpression: s => s.Okul)
                .WithMany(navigationExpression: g => g.Ogretmens)
                .HasForeignKey(s => s.OkulId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
