using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route(template: "api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        private ITestService _testService;
        public TestController(ITestService testService)
        {
            _testService = testService;
        }
        [HttpPost(template: "testebasla")]
        public ActionResult TesteBasla(int kullaniciId)
        {
            var result = _testService.TesteBasla(kullaniciId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest("Test verisi dönmedi.");
        }
        [HttpGet(template: "getpuangrafik")]
        public ActionResult getpuangrafik(int kullaniciId)
        {
            var result = _testService.GetPuanGrafik(kullaniciId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpGet(template: "gettestbitistarih")]
        public ActionResult GetTestBitisTarih(int kullaniciId)
        {
            var result = _testService.GetTestBitisTarih(kullaniciId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpGet(template: "puangetir")]
        public ActionResult PuanGetir(int testId)
        {
            var result = _testService.PuanGetir(testId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpGet(template: "gettestistatistik")]
        public ActionResult gettestistatistik(int testId)
        {
            var result = _testService.GetTestIstatistik(testId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpGet(template: "getgenelistatistikbykullaniciId")]
        public ActionResult getgenelistatistik(int kullaniciId)
        {
            var result = _testService.GetGenelIstatistikByKullaniciId(kullaniciId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpGet(template: "getgenelistatistikbyogrencino")]
        public ActionResult getgenelistatistikbyogrencino(int ogrenciNo)
            {
            var result = _testService.GetGenelIstatistikByOgrenciNo(ogrenciNo);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpPost(template: "gettestlerbykullaniciid")]
        public ActionResult gettestlerbykullaniciid(int kullaniciId)
        {
            var result = _testService.GetTestlerByKullaniciId(kullaniciId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
