using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string ad { get; set; }
        public string soyad { get; set; }
        public string Tel { get; set; }        
        public string kullaniciAdi { get; set; } 
        public bool Status { get; set; } 
        public byte[] PasswordHash { get; set; } 
        public byte[] PasswordSalt { get; set; }      
        public int CurrentRoleId { get; set; }
        [JsonIgnore]
        public virtual Role Role { get; set; }
        [JsonIgnore]
        public virtual UserOperationClaim UserOperationClaim { get; set; }


    }
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne<Role>(navigationExpression: s => s.Role)
                .WithMany(navigationExpression: g => g.Users)
                .HasForeignKey(s => s.CurrentRoleId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
