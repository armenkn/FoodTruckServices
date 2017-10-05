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
        private readonly IBusiness _businessLayer;
        private const string _resourceUrl = "/api/Address/";

        public ContactController(IBusiness businessLayer)
        {
            _businessLayer = businessLayer;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Post([FromBody] ContactInfo contact)
        {
            int contactId = _businessLayer.CreateContact(contact);
            return Created($"{_resourceUrl}{contactId}", contactId);

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ContactInfo contact)
        {
            contact.ContactInfoID = id;
            _businessLayer.UpdateContact(contact);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            ContactInfo contact= _businessLayer.GetContactById(id);
            if (contact != null && contact.ContactInfoID != 0)
                return Ok(contact);
            else
                return NotFound();

        }
    }
}