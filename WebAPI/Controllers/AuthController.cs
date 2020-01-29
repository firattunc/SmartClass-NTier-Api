using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route(template:"api/[controller]")]
    [ApiController]
    public class AuthController:Controller
    {
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost(template:"login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }
            //var result = _authService.CreateAccessToken(userToLogin.Data);
            //if (!result.Success)
            //{
            //    return BadRequest(result.Message);
            //}
            return Ok(userToLogin.Data);
        }
        [HttpPost(template: "register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.kullaniciAdi);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var userToRegister = _authService.Register(userForRegisterDto, userForRegisterDto.sifre);
            if (!userToRegister.Success)
            {
                return BadRequest(userToRegister.Message);
            }
            //var result = _authService.CreateAccessToken(userToRegister.Data);
            //if (!result.Success)
            //{
            //    return BadRequest(result.Message);
            //}
            return Ok(userToRegister.Data);
        }
        [HttpPost(template: "addkullanici")]
        public ActionResult addkullanici(UserForRegisterDto userForRegisterDto)
        {
            var userToRegister = _authService.AddKullanici(userForRegisterDto);
            if (userToRegister.Success)
            {
                return Ok(userToRegister.Message);
            }
            return BadRequest(userToRegister.Message);
        }
        [HttpGet(template: "getokullar")]
        public ActionResult getokullar()
        {
            var result = _authService.GetOkullar();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpGet(template:"getiller")]
        public ActionResult GetIller()
            {
            var result = _authService.GetIller();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpGet(template:"getilcelerbyil")]
        public ActionResult GetIlcelerByIl(int id)
        {
            var result = _authService.GetIlcelerByIl(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);

        }
        [HttpGet(template: "getokullarbyilceid")]
        public ActionResult GetOkullarByIlceId(int id)
        {
            var result = _authService.GetOkullarByIlceId(id);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpGet(template: "getroller")]
        public ActionResult getroller()
        {
            var result = _authService.GetRoller();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
