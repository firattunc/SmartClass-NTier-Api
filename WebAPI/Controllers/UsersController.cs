using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController:ControllerBase
    {
        IUserService _userService;
        
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet(template: "getbyemail")]
        public IActionResult GetList(string email)
        {
            var result = _userService.GetByEmail(email);

            if (result.Success)
            {
                return Ok(result.Data.Id);
            }
            return BadRequest(result.Message);

        }
    }
}
