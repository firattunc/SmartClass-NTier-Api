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
    public class YorumManager : IYorumService
    {
        private IYorumDal _yorumDal;
        private ITestSonucDal _testSonucDal;
        private IOgrenciDal _ogrenciDal;
        public YorumManager(IYorumDal yorumDal, ITestSonucDal testSonucDal,IOgrenciDal ogrenciDal)
        {
            _yorumDal = yorumDal;
            _testSonucDal = testSonucDal;
            _ogrenciDal = ogrenciDal;
        }
        public IDataResult<List<Yorum>> GetYorumlar(int testId)
        {
            var yorumlar = _testSonucDal.GetQueryable(x => x.Id == testId).Include(x => x.Yorums).SelectMany(X => X.Yorums).ToList();
            if (yorumlar!=null)
            {
                return new SuccessDataResult<List<Yorum>>(yorumlar);
            }
            return new ErrorDataResult<List<Yorum>>("Yorumlar getirilemedi.");
        }


        public IResult YorumSil(int yorumId)
        {
            try
            {
                _yorumDal.Delete(new Yorum { Id = yorumId });
                return new SuccessResult("Yorum silindi.");
            }
            catch (Exception)
            {
                return new ErrorResult("Yorum silinemedi.");
            }

        }

        public IResult YorumYap(Yorum yorum)
        {

                var ogrenciId = _ogrenciDal.Get(x => x.UserId == yorum.OgrenciId).Id;
                yorum.OgrenciId = ogrenciId;
                yorum.Tarih = DateTime.Now;
                _yorumDal.Add(yorum);
                return new SuccessResult("Yorum eklendi.");

        }
    }
}
