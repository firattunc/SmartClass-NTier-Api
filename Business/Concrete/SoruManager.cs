using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class SoruManager : ISoruService
    {
        private ISoruDal _soruDal;
        private IAltBasliklarDal _altBasliklarDal;
        private ISoruAltBaslikDal _soruAltBaslikDal;
        private IDersDal _dersDal;
        private IKonuDal _konuDal;
        public SoruManager(ISoruDal soruDal,IAltBasliklarDal altBasliklarDal, ISoruAltBaslikDal soruAltBaslikDal, IDersDal dersDal, IKonuDal konuDal)
        {
            _soruDal = soruDal;
            _altBasliklarDal = altBasliklarDal;
            _soruAltBaslikDal = soruAltBaslikDal;
            _konuDal = konuDal;
            _dersDal = dersDal;
        }

        public IDataResult<List<AltBasliklar>> GetAltBasliklarByKonuId(int konuId)
        {
            var altbasliklar = _altBasliklarDal.GetList(x=>x.KonuId==konuId).ToList();
            return new SuccessDataResult<List<AltBasliklar>>(altbasliklar);
        }

        public IDataResult<List<Ders>> GetDersler()
        {
            var dersler = _dersDal.GetList().ToList();
            return new SuccessDataResult<List<Ders>>(dersler);
        }

        public IDataResult<List<Konu>> GetKonular()
        {
            var konular = _konuDal.GetList().ToList();
            return new SuccessDataResult<List<Konu>>(konular);
        }

        public IDataResult<List<SoruListeleDto>> GetSorular()
        {   
            List<SoruListeleDto> soruListeleDtos = new List<SoruListeleDto>();
            var result = _soruDal.GetQueryable().Include(x => x.Konu).Include(x=>x.Ders).Include(x => x.SoruAltBasliks).ToList();                       
            foreach (var soru in result)
            {
                var altbaslikAdi = _altBasliklarDal.Get(x => x.Id == soru.SoruAltBasliks.FirstOrDefault().AltBaslikId).AltBaslikAdi;
                soruListeleDtos.Add(new SoruListeleDto { 
                    altBaslikAdi= altbaslikAdi,
                    dersAdi=soru.Ders.DersAdi,
                    konuAdi=soru.Konu.KonuAdi,
                    soruId=soru.Id,
                    Cevap=soru.Cevap
                });
            }
            return new SuccessDataResult<List<SoruListeleDto>>(soruListeleDtos);
        }

        public IResult SoruEkle(SoruEkleDto soruEkleDto)
        {
            try
            {
                var soruId=_soruDal.AddAndGetId(new Soru {
                    Cevap=soruEkleDto.Cevap,
                    DersId=soruEkleDto.DersId,
                    ImgUrl=soruEkleDto.ImgUrl,
                    KonuId=soruEkleDto.KonuId
                });
                _soruAltBaslikDal.Add(new SoruAltBaslik {AltBaslikId=soruEkleDto.AltBaslikId,SoruId=soruId } );
                return new SuccessResult("Soru eklendi.");
            }
            catch (Exception)
            {
                return new ErrorResult("Soru eklenemedi.");
            }
            
        }

        public IResult SoruSil(int soruId)
        {
            try
            {
                _soruDal.Delete(new Soru { Id = soruId });
                return new SuccessResult("Soru silindi");
            }
            catch (Exception)
            {

                return new ErrorResult("Soru silinemedi");
            }

        }
    }
}
