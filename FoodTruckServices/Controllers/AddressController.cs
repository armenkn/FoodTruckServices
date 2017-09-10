using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FoodTruckServices.DataAccessLayer;
using FoodTruckServices.Model;
using FoodTruckServices.Interfaces;
using System.Net;
using Microsoft.AspNetCore.Http;

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
            Address address = _dataAccess.GetAddressById(id);
            if (address != null && address.AddressID != 0)
                return Ok(address);
            else
                return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Address address)
        {
            int AddressId = await _dataAccess.CreateAddress(address);
            return Created($"{_resourceUrl}{AddressId}", AddressId);
        }

        /// <summary>
        /// Only companies basic information can be updated by this method.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Address address)
        {
            _dataAccess.UpdateAddress(address);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var databaseResponse = DatabaseResponse.None;
            databaseResponse = _dataAccess.DeleteAddress(id);
            var response = new ContentResult();

            switch (databaseResponse)
            {
                case DatabaseResponse.None:
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    return response;
                case DatabaseResponse.Success:
                    return Ok();
                case DatabaseResponse.CannotDelete:
                    response.StatusCode = StatusCodes.Status409Conflict;
                    response.Content = "Cannot delete address";
                    return response;
                default:
                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    return response;
            }

        }
    }
}