using Domain.User;
using Infrastructure.auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrizeBondChecker.Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PrizeBondChecker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        [Route("getAllUsers")]
        public async Task<ActionResult<List<Users>>> GetAllUsers()
        {
            return Ok(await  _authService.GetAllUsers());
        }

        [HttpGet]
        [Route("getUserById/{id}")]
        [Authorize]
        public async Task<ActionResult<Users>> GetUserById(Guid id)
        {
            return Ok(await _authService.GetUserById(id));
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<Login>> Login(Login request)
        {
            return Ok(await _authService.LoginAsync(request));
        }


        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<Register>> Register(Register request)
        {
            return Ok(await _authService.RegisterAsync(request));
        }

    }
}
