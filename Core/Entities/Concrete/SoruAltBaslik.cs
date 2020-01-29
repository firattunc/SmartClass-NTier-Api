using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class SoruAltBaslik:IEntity
    {
        public int Id { get; set; }        
        public int AltBaslikId { get; set; }
        public int SoruId { get; set; }
        public virtual AltBasliklar AltBaslik { get; set; }
        public virtual Soru Soru { get; set; }
    }
}

