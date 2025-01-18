using Microsoft.AspNetCore.Mvc;
using Finatech.Security.Service;
using Finatech.Security.Model;

namespace Finatech.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] User user)
        {
            _userService.AddUser(user);
            return Ok();
        }

        [HttpGet("{userId}")]
        public IActionResult GetUser(string userId)
        {
            var user = _userService.GetUser(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] User user)
        {
            _userService.UpdateUser(user);
            return Ok();
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(string userId)
        {
            _userService.DeleteUser(userId);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }
    }
}