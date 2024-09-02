using Library_Web_Application_NET.Server.src.model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(401, "Bad credentials.");
                }
                var user = await userManager.Users.FirstOrDefaultAsync(u => u.Email.Equals(loginRequest.Email));
                if (user == null)
                {
                    return StatusCode(401, "Bad credentials.");
                }

                var result = await signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);
                if (!result.Succeeded)
                {
                    return StatusCode(401, "Bad credentials.");
                }
                return Ok(new AuthenticationResponse()
                {
                    Content = await tokenService.CreateToken(user)
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal error occurred during user creation.");
            }
        } 
    }
}
