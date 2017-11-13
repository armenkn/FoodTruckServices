using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodTruckServices.DataAccessLayer;
using FoodTruckServices.Model;
using FoodTruckServices.Interfaces;

namespace FoodTruckServices.Controllers
{
    [Route("api/[controller]")]
    public class FoodTruckController : Controller
    {
        private readonly IBusiness _businessLayer;
        private const string _resourceUrl = "/api/FoodTruck/";

        public FoodTruckController(IBusiness businessLayer)
        {
            _businessLayer = businessLayer;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var foodTruck = _businessLayer.GetFoodTruckById(id);
            return Ok(foodTruck);
        }


        [HttpPost]
        public IActionResult Post([FromBody]FoodTruck foodTruck)
        {
            var foodTruckId = _businessLayer.CreateFoodTruck(foodTruck);
            return Created($"{_resourceUrl}{foodTruckId}", foodTruckId);
        }

        /// <summary>
        /// Only companies basic information can be updated by this method.
        /// </summary>
        /// <param name="foodTruck"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody]FoodTruck foodTruck)
        {
            _businessLayer.UpdateFoodTruck(foodTruck);
            return Ok();
        }

        /// <summary>
        /// Get a list of all FTC basic info
        /// </summary>
        [HttpGet]
        public IActionResult Search([FromBody]FoodTruckSearchCriteria criteria)
        {
            var searchResult = _businessLayer.SearchFoodTruck(criteria);
            return Ok(searchResult);
        }

        [HttpPut("{id}/Deactivate")]
        public IActionResult Deactivate(int id)
        {
            _businessLayer.DeactivateFoodTruck(id);
            return Ok();

        }

    }
}