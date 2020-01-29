using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Ogrenci:IEntity
    {
        public int Id { get; set; }
        public int No { get; set; }
        public int OkulId { get; set; }
        public int UserId { get; set; }
        public virtual Okul Okul { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Yorum> Yorums { get; set; }
        public virtual ICollection<TestSonuc> TestSonucs { get; set; }
        public virtual ICollection<Cevap> Cevaps { get; set; }
        public virtual ICollection<GenelIstatistik> GenelIstatistiks { get; set; }
        public virtual ICollection<OgrenciOgretmeni> OgrenciOgretmenis { get; set; }
        public Ogrenci()
        {
            Yorums = new HashSet<Yorum>();
            OgrenciOgretmenis = new HashSet<OgrenciOgretmeni>();
            TestSonucs = new HashSet<TestSonuc>();
            Cevaps = new HashSet<Cevap>();
            GenelIstatistiks = new HashSet<GenelIstatistik>();
        }

    }
}
