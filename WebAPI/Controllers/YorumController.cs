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
    public class YorumController:Controller
    {
        private IYorumService _yorumService;
        public YorumController(IYorumService yorumService)
        {
            _yorumService = yorumService;
        }
        [HttpGet(template: "getyorumlar")]
        public ActionResult getyorumlar(int testId)
        {
            var result = _yorumService.GetYorumlar(testId);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpDelete(template: "yorumsil")]
        public ActionResult yorumsil(int yorumId)
        {
            var result = _yorumService.YorumSil(yorumId);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
        [HttpPost(template: "yorumyap")]
        public ActionResult yorumyap([FromBody]Yorum yorum)
        {
            var result = _yorumService.YorumYap(yorum);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
