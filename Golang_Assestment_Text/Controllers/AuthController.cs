


using Microsoft.AspNetCore.Mvc;

[ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            var result = _authService.Register(user);
            if (result == "User already exists.")
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            var token = _authService.Login(user.Email, user.PasswordHash);
            if (token == "Invalid email or password.")
            {
                return Unauthorized(token);
            }
            return Ok(new { Token = token });
        }
    }

