using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Ders:IEntity
    {
        public int Id { get; set; }
        public string DersAdi { get; set; }
        //public virtual ICollection<Konu> Konus { get; set; }
        public virtual ICollection<Soru> Sorus { get; set; }        
        public Ders()
        {
            //Konus = new HashSet<Konu>();
            Sorus = new HashSet<Soru>();
        }
    }
}
