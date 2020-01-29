using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class SoruListeleDto:IDto
    {
        public int soruId { get; set; }
        public string dersAdi { get; set; }
        public string konuAdi { get; set; }
        public string altBaslikAdi { get; set; }
        public string Cevap { get; set; }
    }
}
