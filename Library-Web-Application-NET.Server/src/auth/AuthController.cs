using Library_Web_Application_NET.Server.src.auth.data;
using Library_Web_Application_NET.Server.src.auth.Interface;
using Library_Web_Application_NET.Server.src.model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Library_Web_Application_NET.Server.src.auth
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly ITokenService tokenService;

        private readonly UserManager<User> userManager;

        private readonly SignInManager<User> signInManager;

        private readonly IAuthService authService;

        public AuthController
        (
            UserManager<User> userManager,
            ITokenService tokenSerivce,
            SignInManager<User> signInManager,
            IAuthService authService
        ) {
            this.userManager = userManager;
            this.tokenService = tokenSerivce;
            this.signInManager = signInManager;
            this.authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] data.RegisterRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new AuthenticationResponse() { Content = "User already exists" });
                }
                return Ok(await authService.Register(request));
            } 
            catch (Exception)
            {
                return BadRequest(new AuthenticationResponse() { Content = "User already exists"});
            }

        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Login(data.LoginRequest loginRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(401, "Bad credentials.");
                }
                return Ok(await authService.Login(loginRequest));
            }
            catch (Exception)
            {
                return StatusCode(401, "Bad credentials.");
            }
        } 
    }
}
