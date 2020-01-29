using Business.Abstract;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route(template: "api/[controller]")]
    [ApiController]
    public class CevapController:Controller
    {
        private ICevapService _cevapService;
        public CevapController(ICevapService cevapService)
        {
            _cevapService = cevapService;
        }
        [HttpPost(template:"cevapla")]
        public ActionResult Cevapla(Cevap cevap)
        {
            var result = _cevapService.Cevapla(cevap);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result);
        }
    }
}
