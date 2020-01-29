using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class TestModel
    {
        public List<Soru> Sorular { get; set; }
        public int soruNo { get; set; }
        public int soruSayisi { get; set; }
    }
}
