using Microsoft.AspNetCore.Mvc;
using APIWithASPNETCore.Service;
using APIWithASPNETCore.Data.VO;
using Tapioca.HATEOAS;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authorization;

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
        [Produces("application/json")]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]        
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]        
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
        }

        // GET api/persons/5
        [HttpGet("{id}", Name = "GetPersonId")]
        [Produces("application/json")]
        [ProducesResponseType((200), Type = typeof(PersonVO))]        
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(int id)
        {
            var person = _personService.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        // GET api/find-by-name?FirstName=marcos?LastName=rafael
        [HttpGet("find-by-name")]
        [Produces("application/json")]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetByName([FromQuery] string firstName, [FromQuery] string lastName)
        {
            return Ok(_personService.FindByName(firstName,lastName));
        }

        // GET api/find-by-name?FirstName=marcos?LastName=rafael
        [HttpGet("find-with-paged-search/{sortDirection}/{pageSize}/{page}")]
        [Produces("application/json")]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetPagedSearch([FromQuery] string name, [FromQuery] string sortDirection, [FromQuery] int pageSize, [FromQuery] int page)
        {
            return Ok(_personService.FindWidthPagedSearch(name, sortDirection, pageSize, page));
        }


        // POST api/persons
        [HttpPost(Name = "GetPersonPost")]
        [Produces("application/json")]
        [ProducesResponseType((201), Type = typeof(PersonVO))]                
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return new OkObjectResult(_personService.Create(person));
        }

        // PUT api/values/5
        [HttpPut(Name = "GetPersonPut")]
        [Produces("application/json")]
        [ProducesResponseType((202), Type = typeof(PersonVO))]        
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
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

        // PATCH api/values/5
        [HttpPatch(Name = "GetPersonPatch")]
        [Produces("application/json")]
        [ProducesResponseType((202), Type = typeof(PersonVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Patch([FromBody] PersonVO person)
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
        [Produces("application/json")]        
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(int id)
        {
            _personService.Delete(id);
            return NoContent();
        }
    }
}
