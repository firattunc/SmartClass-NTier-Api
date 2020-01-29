using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class UserForRegisterDto:IDto
    {
        public string kullaniciAdi { get; set; }
        public string sifre { get; set; }
        public string ad { get; set; }
        public string soyad { get; set; }
        public int no { get; set; }
        public int selectedIlId { get; set; }
        public int selectedOkulId { get; set; }
        public int selectedIlceId { get; set; }
        public int CurrentRoleId { get; set; }

    }
}
