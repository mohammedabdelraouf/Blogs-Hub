using BlogsAPI.Models;
using BlogsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] UserDTO registerDTO)
        {
            try
            {
                _authService.Register(registerDTO.Email, registerDTO.Password, registerDTO.Role);
                return Ok(new { Message = "User registered successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] UserDTO loginDTO)
        {
            try
            {
                var token = _authService.Login(loginDTO.Email, loginDTO.Password);
                if (token == null)
                {
                    return Unauthorized(new { Message = "Invalid credentials" });
                }
                else
                {
                    return Ok(new { Token = token });
                }
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }

    }
}
