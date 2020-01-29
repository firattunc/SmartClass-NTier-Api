using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class SoruEkleDto:IDto
    {
        public string ImgUrl { get; set; }
        public string Cevap { get; set; }
        public int DersId { get; set; }
        public int KonuId { get; set; }
        public int AltBaslikId { get; set; }        

    }
}
