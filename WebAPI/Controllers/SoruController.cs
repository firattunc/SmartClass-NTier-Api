using Business.Abstract;
using Core.Entities.Concrete;
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
    public class SoruController:Controller
    {
        private ISoruService _soruService;
        public SoruController(ISoruService soruService)
        {
            _soruService = soruService;
        }
        [HttpPost("getsorular")]
        public ActionResult GetSorular()
        {
            var result = _soruService.GetSorular();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("soruekle")]
        public ActionResult soruekle(SoruEkleDto soruEkleDto)
        {
            var result = _soruService.SoruEkle(soruEkleDto);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getaltbasliklarbykonuid")]
        public ActionResult getaltbasliklarbykonuid(int konuId)
        {
            var result = _soruService.GetAltBasliklarByKonuId(konuId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getdersler")]
        public ActionResult getdersler()
        {
            var result = _soruService.GetDersler();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getkonular")]
        public ActionResult getkonular()
        {
            var result = _soruService.GetKonular();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("sorusil")]
        public ActionResult sorusil(int soruId)
        {
            var result = _soruService.SoruSil(soruId);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
