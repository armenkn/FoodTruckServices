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
    public class ContactController : Controller
    {
        private readonly IBusiness _dataAccess;
        private const string _resourceUrl = "/api/Address/";

        public ContactController(IBusiness dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Post([FromBody] ContactInfo contact)
        {
            int contactId = _dataAccess.CreateContact(contact);
            return Created($"{_resourceUrl}{contactId}", contactId);

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ContactInfo contact)
        {
            contact.ContactInfoID = id;
            _dataAccess.UpdateContact(contact);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ContactInfo contact= _dataAccess.GetContactById(id);
            if (contact != null && contact.ContactInfoID != 0)
                return Ok(contact);
            else
                return NotFound();

        }
    }
}