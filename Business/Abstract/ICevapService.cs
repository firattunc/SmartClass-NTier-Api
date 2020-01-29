using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICevapService
    {
        IResult Cevapla(Cevap cevap); 
    }
}
