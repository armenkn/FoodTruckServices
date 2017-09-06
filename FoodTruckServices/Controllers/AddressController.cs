using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FoodTruckServices.DataAccessLayer;
using FoodTruckServices.Model;
using FoodTruckServices.Interfaces;

namespace FoodTruckServices.Controllers
{
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        private readonly IBusiness _dataAccess;
        private const string _resourceUrl = "/api/Address/";

        public AddressController(IBusiness dataAccess)
        {
            _dataAccess = dataAccess;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Address Address = _dataAccess.GetAddressById(id);
            return Ok(Address);
        }


        [HttpPost]
        public IActionResult Post([FromBody]Address address)
        {
            int AddressId = _dataAccess.CreateAddress(address);
            return Created($"{_resourceUrl}{AddressId}", AddressId);
        }

        /// <summary>
        /// Only companies basic information can be updated by this method.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody]Address address)
        {
            _dataAccess.UpdateAddress(address);
            return Ok();
        }

    }
}