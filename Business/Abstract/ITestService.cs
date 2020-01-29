using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ITestService
    {
        IDataResult<TestModel> TesteBasla(int kullaniciId);
        IDataResult<DateTime> GetTestBitisTarih(int kullaniciId);
        IDataResult<string> PuanGetir(int testId);
        IDataResult<List<TestIstatistikModel>> GetTestIstatistik(int testId);
        IDataResult<List<PuanGrafikModel>> GetPuanGrafik(int kullaniciId);
        IDataResult<List<TestIstatistikModel>> GetGenelIstatistikByKullaniciId(int kullaniciId);
        IDataResult<List<TestIstatistikModel>> GetGenelIstatistikByOgrenciNo(int ogrenciNo);
        IDataResult<List<TestSonuc>> GetTestlerByKullaniciId(int kullaniciId);
    }
}
