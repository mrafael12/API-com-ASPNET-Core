using Microsoft.AspNetCore.Mvc;
using APIWithASPNETCore.Service;
using Microsoft.AspNetCore.Authorization;
using APIWithASPNETCore.Model;
using APIWithASPNETCore.Data.VO;

namespace APIWithASPNETCore.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:ApiVersion}")]
    public class FileController : Controller
    {
        private IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        // GET api/file                
        [HttpGet]        
        [ProducesResponseType((200), Type = typeof(byte []))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize("Bearer")]
        public IActionResult GetPDFFile()
        {            
            byte[] buffer = _fileService.GetPDFFile();

            if( buffer != null)
            {
                HttpContext.Response.ContentType = "application/pdf";
                HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());
                HttpContext.Response.Body.Write(buffer, 0, buffer.Length);
            }
            return new ContentResult();
        }        
    }
}
