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

        private readonly UserManager<User> userManager;

        public AuthController(UserManager<User> userManager) {
            this.userManager = userManager;
        }

        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody])
    }
}
