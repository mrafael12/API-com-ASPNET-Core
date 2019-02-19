using Microsoft.AspNetCore.Mvc;
using APIWithASPNETCore.Service;
using APIWithASPNETCore.Data.VO;
using Tapioca.HATEOAS;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace APIWithASPNETCore.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:ApiVersion}")]    
    public class PersonsController : Controller
    {
        private IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        // GET api/persons
        [HttpGet(Name = "GetPerson")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [TypeFilter(typeof(HyperMediaFilter))]        
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
        }

        // GET api/persons/5
        [HttpGet("{id}", Name = "GetPersonId")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(int id)
        {
            var person = _personService.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        // POST api/persons
        [HttpPost(Name = "GetPersonPost")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return new OkObjectResult(_personService.Create(person));
        }

        // PUT api/values/5
        [HttpPut(Name = "GetPersonPut")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null)
            {
                return BadRequest();
            }

            var updatedPerson = _personService.Update(person);

            if (updatedPerson == null)
            {
                return NoContent();
            }

            return new OkObjectResult(updatedPerson);
        }

        // DELETE api/persons/5
        [HttpDelete("{id}", Name = "GetPersonDelete")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(int id)
        {
            _personService.Delete(id);
            return NoContent();
        }
    }
}
