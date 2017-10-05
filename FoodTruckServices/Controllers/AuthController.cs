using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FoodTruckServices.Interfaces;
using FoodTruckServices.Model;

namespace FoodTruckServices.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IBusiness _businessLayer;
        private const string _resourceUrl = "/api/Auth/";

        public AuthController(IBusiness businessLayer)
        {
            _businessLayer = businessLayer;
        }

        [HttpPost("login/{username}/{password}")]
        public IActionResult Login(string username, string password)
        {
            var loginResult = _businessLayer.Login(username, password);
            if (loginResult == null || loginResult.LoginResult != UserLoginResultEnum.Success)
                return Unauthorized();

            return Ok(loginResult.JWT);
        }
    }
}