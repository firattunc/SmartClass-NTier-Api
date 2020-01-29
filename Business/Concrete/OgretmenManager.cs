using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class OgretmenManager : IOgretmenService
    {
        private IOgretmenDal _ogretmenDal;
        private IOgrenciOgretmeniDal _ogrenciOgretmeniDal;
        private IOgrenciDal _ogrenciDal;
        public OgretmenManager(IOgretmenDal ogretmenDal,IOgrenciOgretmeniDal ogrenciOgretmeniDal, IOgrenciDal ogrenciDal)
        {
            _ogrenciOgretmeniDal = ogrenciOgretmeniDal;
            _ogretmenDal = ogretmenDal;
            _ogrenciDal = ogrenciDal;
        }
        public IDataResult<List<OgrenciOgretmeniDto>> GetOgrenciOgretmeniList(int kullaniciId)
        {
            List<OgrenciOgretmeniDto> ogrenciOgretmeniDtos = new List<OgrenciOgretmeniDto>();
            var result = _ogrenciOgretmeniDal.GetQueryable().Include(x => x.Ogretmen).Include(x=>x.Ogrenci.User).Where(X => X.Ogretmen.UserId == kullaniciId).ToList();
            foreach (var ogrenciOgretmeni in result)
            {
                ogrenciOgretmeniDtos.Add(new OgrenciOgretmeniDto
                {
                    ogrenciAdSoyAd = ogrenciOgretmeni.Ogrenci.User.ad + ogrenciOgretmeni.Ogrenci.User.soyad,
                    ogrenciOgretmeniId=ogrenciOgretmeni.Id,
                    ogrenciNo= ogrenciOgretmeni.Ogrenci.No
                });
            }
            if (ogrenciOgretmeniDtos.Count > 0)
                return new SuccessDataResult<List<OgrenciOgretmeniDto>>(ogrenciOgretmeniDtos);
            else
                return new ErrorDataResult<List<OgrenciOgretmeniDto>>("Size kayıtlı öğrenci bulunamadı.Dilerseniz öğrenci ekleyebilirsiniz.");            
        }

        public IResult OgrenciEkle(OgrenciOgretmeniEkleDto ogrenciOgretmeniEkleDto)
        {
            try
            {
                var ogrenci = _ogrenciDal.Get(x => x.No == ogrenciOgretmeniEkleDto.ogrenciNo);
                if (ogrenci==null)
                {
                    return new ErrorResult("Bu öğrenci numarasıyla kayıtlı öğrenci bulunamadı.");
                }
                var ogretmenId = _ogretmenDal.Get(x => x.UserId == ogrenciOgretmeniEkleDto.kullaniciId).Id;

                var ogrenciOgretmeniResult = _ogrenciOgretmeniDal.GetQueryable().Include(x => x.Ogrenci)
                    .Where(x => x.Ogrenci.No == ogrenciOgretmeniEkleDto.ogrenciNo && x.OgretmenId == ogretmenId).FirstOrDefault();
                if (ogrenciOgretmeniResult!=null)
                {
                    return new ErrorResult("Bu öğrenci zaten size kayıtlı");
                }
                
                _ogrenciOgretmeniDal.Add(new OgrenciOgretmeni { 
                    OgrenciId= ogrenci.Id,
                    OgretmenId=ogretmenId
                });
                return new SuccessResult("Öğrenci başarıyla eklendi");
            }
            catch (Exception)
            {
                return new ErrorResult("Öğrenci eklenemedi.");
            }
        }

        public IResult OgrenciSil(int ogrenciOgretmeniId)
        {
            try
            {
                _ogrenciOgretmeniDal.Delete(new OgrenciOgretmeni {Id=ogrenciOgretmeniId });
                return new SuccessResult("Öğrenci başarıyla silindi.");
            }
            catch (Exception)
            {
                return new ErrorResult("Öğrenci silinemedi.");
            }
        }
    }
}
