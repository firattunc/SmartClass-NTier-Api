using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class UserForLoginDto:IDto
    {
        public string kullaniciAdi { get; set; }
        public string sifre { get; set; }
    }
}
