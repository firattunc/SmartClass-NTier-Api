using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Role : IEntity
    {
        public int Id { get; set; }
        public string RoleAdi { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public Role()
        {
            Users = new HashSet<User>();
        }
    }
}
