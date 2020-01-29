using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route(template: "api/[controller]")]
    [ApiController]
    public class OgretmenController : Controller
    {
        private IOgretmenService _ogretmenService;
        public OgretmenController(IOgretmenService ogretmenService)
        {
            _ogretmenService = ogretmenService;
        }
        [HttpGet("getogrenciler")]
        public ActionResult getogrenciler(int kullaniciId)
        {
            var result=_ogretmenService.GetOgrenciOgretmeniList(kullaniciId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("ogrenciekle")]
        public ActionResult addogrenciogretmeni(OgrenciOgretmeniEkleDto ogrenciOgretmeniEkleDto)
        {
            var result = _ogretmenService.OgrenciEkle(ogrenciOgretmeniEkleDto);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpDelete("ogrencisil")]
        public ActionResult deleteogrenciogretmeni(int ogrenciOgretmeniId)
        {
            var result = _ogretmenService.OgrenciSil(ogrenciOgretmeniId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
