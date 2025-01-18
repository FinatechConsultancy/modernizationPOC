using AutoMapper;
using Finatech.Security.Model;
using Finatech.Security.Service;
using Finatech.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Finatech.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public LoginController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto loginDto)
    {
        var user = _userService.GetUser(loginDto.UserName);
        if (user == null || user.PasswordHash != loginDto.PasswordHash)
        {
            return Unauthorized(new { Message = "Invalid credentials" });
        }

        // Update last login time
        user.LastLogin = DateTime.UtcNow;
        _userService.UpdateUser(user);

        return Ok(new { Message = "Login successful" });
    }
}