using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ISoruService
    {
        IDataResult<List<SoruListeleDto>> GetSorular();
        IResult SoruEkle(SoruEkleDto soruEkleDto);
        IResult SoruSil(int soruId);
        IDataResult<List<Konu>> GetKonular();
        IDataResult<List<AltBasliklar>> GetAltBasliklarByKonuId(int konuId);
        IDataResult<List<Ders>> GetDersler();
    }
}
