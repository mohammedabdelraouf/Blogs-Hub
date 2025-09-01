using BlogsAPI.Models;
using BlogsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] UserDTO registerDTO)
        {
            var res = new ResultViewModel();
            try
            {
                _authService.Register(registerDTO.Email, registerDTO.Password, registerDTO.Role);
                res.IsSuccess = true;
                res.Message = "User registered successfully";
                return Ok(res);
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                return BadRequest(res);
            }
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] UserDTO loginDTO)
        {
            var res = new ResultViewModel<string>();
            try
            {
                res.Data = _authService.Login(loginDTO.Email, loginDTO.Password);
                res.IsSuccess = true;
                res.Message = "Login Successful";
                return Ok(res);
               
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                return Unauthorized(res);
            }
        }

    }
}
