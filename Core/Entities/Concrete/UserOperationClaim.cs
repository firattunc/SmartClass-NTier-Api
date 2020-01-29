using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class UserOperationClaim:IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CurrentOperationClaimId { get; set; }
        public virtual OperationClaim OperationClaim { get; set; }
        public virtual User User { get; set; }
    }
    public class UserOperationClaimEntityConfiguration : IEntityTypeConfiguration<UserOperationClaim>
    {
        public void Configure(EntityTypeBuilder<UserOperationClaim> builder)
        {
            builder.HasOne<OperationClaim>(navigationExpression: s => s.OperationClaim)
                .WithMany(navigationExpression: g => g.UserOperationClaim)
                .HasForeignKey(s => s.CurrentOperationClaimId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            builder.HasOne<User>(s => s.User)
               .WithOne(ad => ad.UserOperationClaim)
               .HasForeignKey<UserOperationClaim>(ad => ad.UserId);
        }
    }

}
