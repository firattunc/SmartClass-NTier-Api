using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Okul:IEntity
    {
        public int Id { get; set; }
        public string OkulAdi { get; set; }
        public string ImgUrl { get; set; }
        public int IlceId { get; set; }
        [JsonIgnore]
        public virtual Ilce Ilce { get; set; }
        [JsonIgnore]
        public virtual ICollection<Ogrenci> Ogrencis { get; set; }
        [JsonIgnore]
        public virtual ICollection<Ogretmen> Ogretmens { get; set; }
        public Okul()
        {
            Ogrencis = new HashSet<Ogrenci>();
            Ogretmens = new HashSet<Ogretmen>();
        }
    }
}
