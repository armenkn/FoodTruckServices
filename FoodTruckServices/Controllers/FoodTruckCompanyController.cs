using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FoodTruckServices.DataAccessLayer;
using FoodTruckServices.DataAccessLayer.Implementations;
using FoodTruckServices.Model;
using System;
using FoodTruckServices.Interfaces;

namespace FoodTruckServices.Controllers
{
    [Route("api/[controller]")]
    public class FoodTruckCompanyController : Controller
    {
        private readonly IBusiness _dataAccess;
        private const string _resourceUrl = "/api/FoodTruckCompany/";
        public FoodTruckCompanyController(IBusiness dataAccess)
        {
            _dataAccess = dataAccess;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
           var foodTruckCompany = _dataAccess.GetFoodTruckCompanyById(id);
            return Ok(foodTruckCompany);
        }

        [HttpPost]
        public IActionResult Post([FromBody]FoodTruckCompany foodTruckCompany)
        {
            var foodTruckCompanyId = _dataAccess.CreateFoodTruckCompany(foodTruckCompany);
            return Created($"{_resourceUrl}{foodTruckCompanyId}", foodTruckCompanyId);
        }

        /// <summary>
        /// Only companies basic information can be updated by this method.
        /// </summary>
        /// <param name="foodTruckCompany"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody]FoodTruckCompany foodTruckCompany)
        {
            _dataAccess.UpdateFoodTruckCompany(foodTruckCompany);
            return Ok();
        }

        /// <summary>
        /// Get a list of all FTC basic info
        /// </summary>
        [HttpGet]
        public IActionResult Search([FromBody]FoodTruckCompanySearchCriteria criteria)
        {            
            var searchResult = _dataAccess.SearchFoodTruckCompany(criteria);            
            return Ok(searchResult);
        }
        
        [HttpPut("{id}/Deactivate")]
        public IActionResult Deactivate(int id)
        {
            _dataAccess.DeactivateFoodTruckCompany(id);
            return Ok();

        }

    }
}