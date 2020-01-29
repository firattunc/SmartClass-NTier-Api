using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IOgretmenService
    {
        IDataResult<List<OgrenciOgretmeniDto>> GetOgrenciOgretmeniList(int kullaniciId);
        IResult OgrenciEkle(OgrenciOgretmeniEkleDto ogrenciOgretmeniEkleDto);
        IResult OgrenciSil(int ogrenciOgretmeniId);
    }
}
