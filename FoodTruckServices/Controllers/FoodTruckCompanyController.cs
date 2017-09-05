using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FoodTruckServices.DataAccessLayer;
using FoodTruckServices.DataAccessLayer.Implementations;
using FoodTruckServices.Model;
using System;

namespace FoodTruckServices.Controllers
{
    [Route("api/[controller]")]
    public class FoodTruckCompanyController : Controller
    {
        private readonly IDataAccess _dataAccess;
        private const string _resourceUrl = "/api/FoodTruckCompany/";
        public FoodTruckCompanyController(IDataAccess dataAccess)
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
    }
}