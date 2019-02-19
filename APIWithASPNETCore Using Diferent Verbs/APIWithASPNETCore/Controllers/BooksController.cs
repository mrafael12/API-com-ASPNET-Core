using APIWithASPNETCore.Data.VO;
using APIWithASPNETCore.Service;
using Microsoft.AspNetCore.Mvc;
using Tapioca.HATEOAS;

namespace APIWithASPNETCore.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:ApiVersion}")]    
    public class BooksController : Controller
    {
        private IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET api/books
        [HttpGet(Name = "GetBook")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_bookService.FindAll());            
        }        

        // GET api/books/5
        [HttpGet("{id}", Name = "GetBookId")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(int id)
        {
            var book = _bookService.FindById(id);            
            if (book == null) return NotFound();
            return Ok(book);
        }

        // POST api/books
        [HttpPost(Name = "GetBookPost")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();
            return new ObjectResult(_bookService.Create(book));            
        }

        // PUT api/books/5
        [HttpPut(Name = "GetBookPut")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] BookVO book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            var updatedPerson = _bookService.Update(book);            

            if (updatedPerson == null)
            {
                return NoContent();
            }

            return new ObjectResult(updatedPerson);
        }

        // DELETE api/books/5
        [HttpDelete("{id}", Name = "GetBookDelete")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(int id)
        {
            _bookService.Delete(id);            
            return NoContent();
        }
    }
}
