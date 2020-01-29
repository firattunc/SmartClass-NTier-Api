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
    public class TestManager : ITestService
    {
        private ICevapDal _cevapDal;
        private ITestSonucDal _testSonucDal;
        private IOgrenciDal _ogrenciDal;
        private ISoruDal _soruDal;
        private IGenelIstatistikDal _genelIstatistikDal;
        private ISoruAltBaslikDal _soruAltBaslikDal;
        public TestManager(ICevapDal cevapDal, ITestSonucDal testSonucDal, IOgrenciDal ogrenciDal, ISoruDal soruDal, IGenelIstatistikDal genelIstatistikDal,
            ISoruAltBaslikDal soruAltBaslikDal)
        {
            _cevapDal = cevapDal;
            _testSonucDal = testSonucDal;
            _ogrenciDal = ogrenciDal;
            _soruDal = soruDal;
            _genelIstatistikDal = genelIstatistikDal;
            _soruAltBaslikDal = soruAltBaslikDal;
        }
        public List<Soru> CozulmeyenSorulariGetir(int id)
        {
            List<Soru> sorular = _soruDal.GetList().ToList();
            List<Cevap> dogruCevaplar = _cevapDal.GetList(x => x.IsTrue == "1" && x.OgrenciId == id).ToList();
            var dogruSorular = dogruCevaplar.Select(x => x.Soru).ToList();
            //Doğru cevapları çıkar
            var result = sorular.Except(dogruSorular).ToList();
            return result;
        }

        public IDataResult<List<TestIstatistikModel>> GetGenelIstatistikByKullaniciId(int kullaniciId)
        {
            try
            {
                var testSonuc = _testSonucDal.GetQueryable().Include(x => x.Ogrenci).Where(x => x.Ogrenci.UserId == kullaniciId).FirstOrDefault();
                if (testSonuc==null)
                {
                    return new ErrorDataResult<List<TestIstatistikModel>>("Daha önce test olunmamış");
                }

                var testIstatistikModel = new List<TestIstatistikModel>();
                var ogrenciId = _ogrenciDal.Get(x => x.UserId == kullaniciId).Id;
                var basariYuzdeleri = _genelIstatistikDal.GetQueryable().Include(x => x.AltBaslik).Where(x => x.OgrenciId == ogrenciId).OrderBy(x => x.DogruSayisi).ToList();
                foreach (var item in basariYuzdeleri)
                {
                    //AltBaslik'tan kaç tane sorulmuş
                    var soruCountAltBaslik = _cevapDal.GetQueryable().Include(X => X.Soru.SoruAltBasliks).
                        Where(x => x.Soru.SoruAltBasliks.FirstOrDefault().AltBaslikId == item.AltBaslikId && x.Ogrenci.Id == ogrenciId).ToList().Count;
                    float yuzde;
                    if (soruCountAltBaslik != 0)
                    {
                        yuzde = ((float)item.DogruSayisi / soruCountAltBaslik);
                    }
                    else
                    {
                        yuzde = 0;
                    }
                    testIstatistikModel.Add(new TestIstatistikModel
                    {
                        basariYuzdesi = yuzde,
                        altBaslikAdi = item.AltBaslik.AltBaslikAdi
                    });
                }
                return new SuccessDataResult<List<TestIstatistikModel>>(testIstatistikModel);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<TestIstatistikModel>>("Hata oluştu" + ex.Message);
            }

        }

        public IDataResult<List<TestIstatistikModel>> GetGenelIstatistikByOgrenciNo(int ogrenciNo)
        {
            try
            {                
                var ogrenci = _ogrenciDal.Get(x => x.No == ogrenciNo);
                if (ogrenci==null)
                {
                    return new ErrorDataResult<List<TestIstatistikModel>>("Bu okul numarasıyla kayıtlı öğrenci yok");
                }
                //Daha önce test olunmuş mu ?
                var testSonuc = _testSonucDal.GetQueryable().Include(x => x.Ogrenci).Where(x => x.Ogrenci.No == ogrenciNo);
                if (testSonuc.Count()==0)
                {
                    return new ErrorDataResult<List<TestIstatistikModel>>("Daha önce test olunmamış.");
                }

                var testIstatistikModel = new List<TestIstatistikModel>();
                var basariYuzdeleri = _genelIstatistikDal.GetQueryable().Include(x => x.AltBaslik).Where(x => x.OgrenciId == ogrenci.Id).OrderBy(x => x.DogruSayisi).ToList();
                foreach (var item in basariYuzdeleri)
                {
                    //AltBaslik'tan kaç tane sorulmuş
                    var soruCountAltBaslik = _cevapDal.GetQueryable().Include(X => X.Soru.SoruAltBasliks).
                        Where(x => x.Soru.SoruAltBasliks.FirstOrDefault().AltBaslikId == item.AltBaslikId && x.Ogrenci.Id == ogrenci.Id).ToList().Count;
                    float yuzde;
                    if (soruCountAltBaslik != 0)
                    {
                        yuzde = ((float)item.DogruSayisi / soruCountAltBaslik);
                    }
                    else
                    {
                        yuzde = 0;
                    }
                    testIstatistikModel.Add(new TestIstatistikModel
                    {
                        basariYuzdesi = yuzde,
                        altBaslikAdi = item.AltBaslik.AltBaslikAdi
                    });
                }

                return new SuccessDataResult<List<TestIstatistikModel>>(testIstatistikModel);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<TestIstatistikModel>>("Hata oluştu" + ex.Message);
            }
        }

        public IDataResult<List<PuanGrafikModel>> GetPuanGrafik(int kullaniciId)
        {
            try
            {
                List<PuanGrafikModel> puanGrafikModel = new List<PuanGrafikModel>();
                var result = _testSonucDal.GetQueryable().Include(x => x.Ogrenci).Where(x => x.Ogrenci.UserId == kullaniciId);                
                if (result.Count() == 0)
                {
                    return new ErrorDataResult<List<PuanGrafikModel>>("Daha önce test olunmamış");
                }

                foreach (var item in result)
                {
                    puanGrafikModel.Add(new PuanGrafikModel { puan = item.DogruSayisi * 1.92, tarih = item.BitisTarih.ToString() });
                }
                return new SuccessDataResult<List<PuanGrafikModel>>(puanGrafikModel);
            }
            catch (Exception)
            {

                return new ErrorDataResult<List<PuanGrafikModel>>("Puan grafiği getirilirken hatayla karşılaşıldı.");
            }

        }

        public IDataResult<DateTime> GetTestBitisTarih(int kullaniciId)
        {
            var result=_testSonucDal.GetQueryable().Include(x => x.Ogrenci.User).Where(x => x.Ogrenci.UserId == kullaniciId).Last().BitisTarih;
            if (result!=null)
            {
                return new SuccessDataResult<DateTime>(result);
            }
            return new ErrorDataResult<DateTime>("Bitiş tarihi getirilirken hatayla karşılaşıldı.");
        }

        public IDataResult<List<TestIstatistikModel>> GetTestIstatistik(int testId)
        {
            try
            {
                var testIstatistikModel = new List<TestIstatistikModel>();
                var testSonuc = _testSonucDal.Get(x => x.Id == testId);
                var ogrenciId = _ogrenciDal.Get(x => x.Id == testSonuc.OgrenciId).Id;
                var basariYuzdeleri = _genelIstatistikDal.GetQueryable().Include(x => x.AltBaslik).Where(x => x.OgrenciId == ogrenciId).OrderBy(x => x.DogruSayisi).ToList();
                int dogruSayisi = 0;
                foreach (var item in basariYuzdeleri)
                {
                    //AltBaslikla ilgili çözülmüş sorular
                    var soruAltBaslik = _cevapDal.GetQueryable().Include(X => X.Soru.SoruAltBasliks).
                            Where(x => x.Soru.SoruAltBasliks.FirstOrDefault().AltBaslikId == item.AltBaslikId && x.Ogrenci.Id == ogrenciId
                            &&x.TestSonucId==testSonuc.Id).ToList();
                    //AltBaslik'tan kaç tane sorulmuş
                    int soruCountAltBaslik = soruAltBaslik.Count;

                    foreach (var cevap in soruAltBaslik)
                    {
                        if (Convert.ToInt32(cevap.IsTrue) == 1)
                        {
                            dogruSayisi++;
                        }
                    }
                    float yuzde;
                    if (soruCountAltBaslik != 0)
                    {
                        yuzde = ((float)dogruSayisi / soruCountAltBaslik);
                    }
                    else
                    {
                        yuzde = 0;
                    }
                    testIstatistikModel.Add(new TestIstatistikModel
                    {
                        basariYuzdesi = yuzde,
                        altBaslikAdi = item.AltBaslik.AltBaslikAdi
                    });
                    dogruSayisi = 0;
                }
                return new SuccessDataResult<List<TestIstatistikModel>>(testIstatistikModel);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<TestIstatistikModel>>("Hata Oluştu" + ex.Message);
            }
           
        }

        public IDataResult<List<TestSonuc>> GetTestlerByKullaniciId(int kullaniciId)
        {
            var result=_testSonucDal.GetQueryable().Include(x => x.Ogrenci).Where(x => x.Ogrenci.UserId == kullaniciId).ToList();
            if (result.Count!=0)
            {
                return new SuccessDataResult<List<TestSonuc>>(result);
            }
            return new ErrorDataResult<List<TestSonuc>>("Testler getirilemedi.");
        }

        public IDataResult<string> PuanGetir(int testId)
        {
            var puan = _testSonucDal.Get(x => x.Id == testId).DogruSayisi * 1.92;
            string result = puan.ToString("0.00");
            return new SuccessDataResult<string>(data:result);
        }

        public IDataResult<TestModel> TesteBasla(int kullaniciId)
        {

            Random random = new Random();
            var tarih = DateTime.Now;
            List<Soru> sorularList = new List<Soru>();
            int soruSayisi = 0;

            var ogrenci = _ogrenciDal.Get(x => x.UserId == kullaniciId);
            if (ogrenci == null)
            { 
                return new ErrorDataResult<TestModel>("Öğrenci bulunamadı.");
            }
            try
            {
                _testSonucDal.Add(new TestSonuc
                {
                    BitisTarih = tarih.AddMinutes(50),
                    DogruSayisi = 0,
                    OgrenciId = ogrenci.Id,
                    Tarih = tarih
                });

            }
            catch (Exception)
            {

                return new ErrorDataResult<TestModel>("Test eklenemedi.");
            }



            var sorular = CozulmeyenSorulariGetir(ogrenci.Id);
            var basariYuzdeleri = _genelIstatistikDal.GetList(X => X.OgrenciId == ogrenci.Id).ToList();

            //Ara Toplam Ortalamasını Bul
            float araToplam = 0;
            float araToplamOrt = 0;
            foreach (var item in basariYuzdeleri)
            {
                //AltBaslik'tan kaç tane sorulmuş                
                var soruCountAltBaslik = _cevapDal.GetQueryable().Include(X => X.Soru.SoruAltBasliks).
                    Where(x => x.Soru.SoruAltBasliks.FirstOrDefault().AltBaslikId == item.AltBaslikId &&  x.Ogrenci.Id == ogrenci.Id).ToList().Count;
                float yuzde;
                if (soruCountAltBaslik != 0)
                {
                    yuzde = 1 - ((float)item.DogruSayisi / soruCountAltBaslik);
                }
                else
                {
                    yuzde = 1;
                }
                araToplam += yuzde;
            }
            araToplamOrt = 50 / araToplam;


            //Soruları başarı oranlarına göre ata
            foreach (var item in basariYuzdeleri)   
            {
                //AltBasliktaki sorular                
                List<Soru> sorularAltBaslik = _soruDal.GetQueryable().Include(x => x.SoruAltBasliks).Where(x => x.SoruAltBasliks.FirstOrDefault().AltBaslikId == item.AltBaslikId).ToList();
                //AltBaslik'tan kaç tane sorulmuş
                var soruCountAltBaslik = _cevapDal.GetQueryable().Include(X => X.Soru.SoruAltBasliks).
                    Where(x => x.Soru.SoruAltBasliks.FirstOrDefault().AltBaslikId == item.AltBaslikId && x.Ogrenci.Id == ogrenci.Id).ToList().Count;
                double yuzde;
                if (soruCountAltBaslik != 0)
                {
                    yuzde = 1 - ((float)item.DogruSayisi / soruCountAltBaslik);
                }
                else
                {
                    yuzde = 1;
                }

                double DoublesoruSayisi = yuzde * araToplamOrt;
                int yuvarlananSoruSayisi = Convert.ToInt32(Math.Round(DoublesoruSayisi));
                //Bir tane garanti sor
                if (yuvarlananSoruSayisi == 0)
                {
                    yuvarlananSoruSayisi = 1;
                }

                for (int i = 0; i < yuvarlananSoruSayisi; i++)
                {
                    var index = random.Next(0, sorularAltBaslik.Count);
                    try
                    {
                        sorularList.Add(sorularAltBaslik[index]);
                        soruSayisi++;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        continue;
                    }

                    sorularAltBaslik.RemoveAt(index);
                }
            }
            sorular = sorular.Except<Soru>(sorularList).ToList();
            while (sorularList.Count < 52)
            {
                var index = random.Next(0, sorular.Count);
                if (sorular.Count == 0)
                {
                    return new SuccessDataResult<TestModel>(new TestModel
                    {
                        soruSayisi = soruSayisi,
                        Sorular = sorularList,
                        soruNo = 0
                    });
                }
                sorularList.Add(sorular[index]);
                soruSayisi++;
                sorular.RemoveAt(index);
            }
            return new SuccessDataResult<TestModel>(new TestModel
            {
                Sorular = sorularList,
                soruNo = 0,
                soruSayisi = soruSayisi
            });

        }
    }
}
