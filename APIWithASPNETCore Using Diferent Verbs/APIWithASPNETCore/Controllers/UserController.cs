using Microsoft.AspNetCore.Mvc;
using APIWithASPNETCore.Service;
using Microsoft.AspNetCore.Authorization;
using APIWithASPNETCore.Model;

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
        public object Post([FromBody] User user)
        {
            if (user == null) return BadRequest();
            return _userService.FindByLogin(user);
        }        
    }
}
