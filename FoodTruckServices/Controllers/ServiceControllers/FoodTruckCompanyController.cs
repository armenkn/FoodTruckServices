using Microsoft.AspNetCore.Mvc;
using FoodTruckServices.Model;
using FoodTruckServices.Interfaces;

namespace FoodTruckServices.Controllers
{
    [Route("api/[controller]")]
    public class FoodTruckCompanyController : Controller
    {
        private readonly IBusiness _businessLayer;
        private const string _resourceUrl = "/api/FoodTruckCompany/";
        public FoodTruckCompanyController(IBusiness businessLayer)
        {
            _businessLayer = businessLayer;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var foodTruckCompany = _businessLayer.GetFoodTruckCompanyById(id);
            return Ok(foodTruckCompany);
        }

        [HttpPost]
        public IActionResult Post([FromBody]FoodTruckCompany foodTruckCompany)
        {
            var foodTruckCompanyId = _businessLayer.CreateFoodTruckCompany(foodTruckCompany);
            return Created($"{_resourceUrl}{foodTruckCompanyId}", foodTruckCompanyId);
        }

        /// <summary>
        /// Only companies basic information can be updated by this method.
        /// </summary>
        /// <param name="foodTruckCompany"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]FoodTruckCompany foodTruckCompany)
        {
            foodTruckCompany.FoodTruckCompanyId = id;
            _businessLayer.UpdateFoodTruckCompany(foodTruckCompany);
            return Ok();
        }

        /// <summary>
        /// Get a list of all FTC basic info
        /// </summary>
        [HttpGet("search")]
        public IActionResult Search(FoodTruckCompanySearchCriteria criteria)
        {
            var searchResult = _businessLayer.SearchFoodTruckCompany(criteria);
            return Ok(searchResult);
        }
    }
}