using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Il:IEntity
    {
        public int Id { get; set; }
        public string IlAd { get; set; }
        public virtual ICollection<Ilce> Ilces { get; set; }
        public Il()
        {
            Ilces = new HashSet<Ilce>();
        }
    }
}
