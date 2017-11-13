using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FoodTruckServices.Interfaces;
using FoodTruckServices.Filters;

namespace FoodTruckServices.Controllers.AppControllers
{
    [ServiceFilter(typeof(AuthFilter))]
    [Route("app/foodtruck")]
    public class FoodTruckAppController : Controller
    {
        private readonly IBusiness _businessLayer;
        private const string _resourceUrl = "/app/FoodTruckApp/";

        public FoodTruckAppController(IBusiness businessLayer)
        {
            _businessLayer = businessLayer;
        }

        [HttpPut("activate/{latitude}/{longitude}")]
        public IActionResult Activate(decimal latitude, decimal longitude)
        {
            _businessLayer.ActivateFoodTruck(latitude, longitude);

            return Ok();
        }
    }
}