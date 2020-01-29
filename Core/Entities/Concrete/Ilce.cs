using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Ilce:IEntity
    {
        public int Id { get; set; }
        public string IlceAd { get; set; }
        public int IlId { get; set; }
        public virtual Il Il { get; set; }
       
    }
    public class IlceEntityConfiguration : IEntityTypeConfiguration<Ilce>
    {
        public void Configure(EntityTypeBuilder<Ilce> builder)
        {
            builder.HasOne<Il>(navigationExpression: s => s.Il)
                .WithMany(navigationExpression: g => g.Ilces)
                .HasForeignKey(s => s.IlId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
