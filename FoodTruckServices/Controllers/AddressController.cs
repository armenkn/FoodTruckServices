using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FoodTruckServices.Model;
using FoodTruckServices.Interfaces;
using Microsoft.AspNetCore.Http;
using FoodTruckServices.Model.Exceptions;
using NLog;
using FoodTruckServices.Filters;
using Microsoft.Extensions.Logging;

namespace FoodTruckServices.Controllers
{
    [ServiceFilter(typeof(AuthFilter))]
    [Route("api/[controller]")]
    public class AddressController : Controller
    {
        private readonly IBusiness _businessLayer;
        private const string _resourceUrl = "/api/Address/";
        private readonly ILogger<AddressController> _logger;

        public AddressController(IBusiness businessLayer, ILogger<AddressController> logger)
        {
            _businessLayer = businessLayer;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Address address = _businessLayer.GetAddressById(id);
            if (address != null && address.AddressID != 0)
                return Ok(address);
            else
                return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Address address)
        {
            int addressId = await _businessLayer.CreateAddress(address);
            return Created($"{_resourceUrl}{addressId}", addressId);
        }

        /// <summary>
        /// Only companies basic information can be updated by this method.
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Address address)
        {
            address.AddressID = id;
            try
            {
                await _businessLayer.UpdateAddress(address);
                return Ok();
            }
            catch (AuthenticationException authEx)
            {
                _logger.LogError($"Location:{_resourceUrl}, Exception: {authEx.GetType().ToString()}, Message: {authEx.Message}");
                return Unauthorized();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Location:{_resourceUrl}, Exception: {ex.GetType().ToString()}, Message: {ex.Message}");
                return new ContentResult()
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var databaseResponse = DatabaseResponse.None;
            databaseResponse = _businessLayer.DeleteAddress(id);
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