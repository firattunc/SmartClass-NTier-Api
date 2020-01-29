using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class OkulDto:IDto
    {
        public string OkulAdi { get; set; }
        public string ImgUrl { get; set; }
        public string IlAdi { get; set; }
    }
}
