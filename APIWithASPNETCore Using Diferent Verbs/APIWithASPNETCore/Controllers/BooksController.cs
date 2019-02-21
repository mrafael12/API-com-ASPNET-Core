using APIWithASPNETCore.Data.VO;
using APIWithASPNETCore.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        [Produces("application/json")]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_bookService.FindAll());            
        }        

        // GET api/books/5
        [HttpGet("{id}", Name = "GetBookId")]
        [Produces("application/json")]
        [ProducesResponseType((200), Type = typeof(BookVO))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(int id)
        {
            var book = _bookService.FindById(id);            
            if (book == null) return NotFound();
            return Ok(book);
        }

        // POST api/books
        [HttpPost(Name = "GetBookPost")]
        [Produces("application/json")]
        [ProducesResponseType((201), Type = typeof(BookVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();
            return new OkObjectResult(_bookService.Create(book));            
        }

        // PUT api/books/5
        [HttpPut(Name = "GetBookPut")]
        [Produces("application/json")]
        [ProducesResponseType((202), Type = typeof(BookVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] BookVO book)
        {
            if (book == null)
            {
                return BadRequest();
            }

            var updatedBook = _bookService.Update(book);            

            if (updatedBook == null)
            {
                return NoContent();
            }

            return new OkObjectResult(updatedBook);
        }

        // DELETE api/books/5
        [HttpDelete("{id}", Name = "GetBookDelete")]
        [Produces("application/json")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]        
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(int id)
        {
            _bookService.Delete(id);            
            return NoContent();
        }
    }
}
