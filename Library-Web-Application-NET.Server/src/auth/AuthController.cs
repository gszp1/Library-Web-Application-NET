using Library_Web_Application_NET.Server.src.model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Library_Web_Application_NET.Server.src.auth
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly ITokenService tokenService;

        private readonly UserManager<User> userManager;

        

        public AuthController(UserManager<User> userManager, ITokenService tokenSerivce) {
            this.userManager = userManager;
            this.tokenService = tokenSerivce;
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
                var user = new User
                {
                    UserName = request.Email,
                    Email = request.Email,
                    Name = request.Name.IsNullOrEmpty() ? null : request.Name,
                    Surname = request.Surname.IsNullOrEmpty() ? null : request.Surname,
                    JoinDate = DateOnly.FromDateTime(DateTime.Now)
                };
                var createdUser = await userManager.CreateAsync(user, request.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await userManager.AddToRoleAsync(user, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(new AuthenticationResponse()
                        {
                            Content = await tokenService.CreateToken(user)
                        });
                    }
                    else
                    {
                        return StatusCode(500, "Internal error occurred during user creation.");
                    }
                }
                else
                {
                    return StatusCode(500, "Internal error occurred during user creation.");
                }
            }
            catch (Exception e) 
            {
                return StatusCode(500, "Internal error occurred during user creation.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {

        } 
    }
}
