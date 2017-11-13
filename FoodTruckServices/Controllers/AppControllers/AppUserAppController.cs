using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FoodTruckServices.Interfaces;
using FoodTruckServices.Model;

namespace FoodTruckServices.Controllers.AppControllers
{
    [Route("app/user")]
    public class AppUserAppController : Controller
    {
        private readonly IBusiness _businessLayer;
        private const string _resourceUrl = "/app/user/";

        public AppUserAppController(IBusiness businessLayer)
        {
            _businessLayer = businessLayer;
        }

        [HttpPut("updatelocaiton/{isPushNotification}/{latitude}/{longitude}")]
        public IActionResult UpdateUserLocation(bool isPushNotification, decimal latitude, decimal longitude)
        {
            var activeFoodTrucksNearUser = _businessLayer.UpdateAppUserLocation(isPushNotification, latitude, longitude);

            if (isPushNotification)
                return Ok(activeFoodTrucksNearUser);
            else
                return Ok();
        }

        [HttpPost("{latitude}/{longitude}")]
        [HttpPost()]
        public IActionResult Post([FromBody] User user, decimal latitude, decimal longitude)
        {
            Coordination coordination = new Coordination() { Latitude = latitude, Longitude = longitude };

            int userId = _businessLayer.CreateAppUser(user, coordination);
            return Created($"{_resourceUrl}{userId}", userId);
        }
        
    }
}