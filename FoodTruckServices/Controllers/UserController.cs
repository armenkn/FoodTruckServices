using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FoodTruckServices.Model;
using FoodTruckServices.Interfaces;

namespace FoodTruckServices.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IBusiness _dataAccess;
        private const string _resourceUrl = "/api/User/";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            int userId = _dataAccess.CreateUser(user);
            return Created($"{_resourceUrl}{userId}", userId);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _dataAccess.GetUserById(id);
            if (user != null && user.UserId != 0)
                return Ok(user);
            else
                return NotFound();
        }

        [HttpGet("/role/{userRoleId}")]
        public IActionResult GetByRole(int userRoleId)
        {
            List<User> users = _dataAccess.GetUsersByRoleId(userRoleId);
            if(users == null || users.Count == 0)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [HttpPost("/login/{username}/{password}")]
        public IActionResult Login(string username, string password)
        {
            var loginResult = _dataAccess.Login(username, password);
            if (loginResult != UserLoginResultEnum.Success)
                return Unauthorized();
            return Ok();
        }


    }

}