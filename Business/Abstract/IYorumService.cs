using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IYorumService
    {
        IResult YorumYap(Yorum yorum);
        IResult YorumSil(int yorumId);
        IDataResult<List<Yorum>> GetYorumlar(int testId);
    }
}
