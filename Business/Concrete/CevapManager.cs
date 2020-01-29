using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CevapManager : ICevapService
    {
        private ICevapDal _cevapDal;
        private IGenelIstatistikDal _genelIstatistikDal;
        private ISoruDal _soruDal;
        private ITestSonucDal _testSonucDal;        
        public CevapManager(ICevapDal cevapDal, IGenelIstatistikDal genelIstatistikDal,ISoruDal soruDal, ITestSonucDal testSonucDal)
        {
            _cevapDal = cevapDal;
            _genelIstatistikDal = genelIstatistikDal;
            _soruDal = soruDal;
            _testSonucDal = testSonucDal;            
        }
        public IResult Cevapla(Cevap cevap)
        {
            try
            {                  
                var soruCevap = _soruDal.GetQueryable().Include(x => x.SoruAltBasliks).Where(x => x.Id == cevap.SoruId).FirstOrDefault();
                var testSonuc = _testSonucDal.GetQueryable().Include(x => x.Ogrenci.User).Where(x => x.Ogrenci.UserId == cevap.OgrenciId).Last();                
                if (soruCevap.Cevap==cevap.IsTrue)
                {
                    var istatistik = _genelIstatistikDal.GetQueryable().
                        Include(x => x.Ogrenci.User).Where(x => x.Ogrenci.UserId == cevap.OgrenciId && x.AltBaslikId == soruCevap.SoruAltBasliks.FirstOrDefault().AltBaslikId).FirstOrDefault();
                    cevap.IsTrue = "1";
                    testSonuc.DogruSayisi++;
                    istatistik.DogruSayisi++;
                    _testSonucDal.Update(testSonuc);
                    _genelIstatistikDal.Update(istatistik);
                    
                }
                else
                    cevap.IsTrue = "0";

                cevap.OgrenciId = testSonuc.OgrenciId;
                cevap.TestSonucId = testSonuc.Id;
                cevap.Tarih = DateTime.Now;
                
                _cevapDal.Add(cevap);
                return new SuccessResult("Cevap Eklendi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult("Cevap eklenirken hata oluştu."+ex.Message);
            }
           
        }
    }
}
