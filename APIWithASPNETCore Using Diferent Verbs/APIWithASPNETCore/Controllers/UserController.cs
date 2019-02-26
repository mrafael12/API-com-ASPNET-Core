using Microsoft.AspNetCore.Mvc;
using APIWithASPNETCore.Service;
using Microsoft.AspNetCore.Authorization;
using APIWithASPNETCore.Model;
using APIWithASPNETCore.Data.VO;

namespace APIWithASPNETCore.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:ApiVersion}")]    
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }                

        // POST api/persons
        [AllowAnonymous]
        [HttpPost]        
        public object Post([FromBody] UserVO user)
        {
            if (user == null) return BadRequest();
            return _userService.FindByLogin(user);
        }

        // POST api/user
        [AllowAnonymous]
        [HttpPost("save")]
        [Produces("application/json")]        
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        //[Authorize("Bearer")]        
        public IActionResult Save([FromBody] User user)
        {
            if (user == null) return BadRequest();
            return new OkObjectResult(_userService.Create(user));
        }
    }
}
