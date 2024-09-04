using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Web_Application_NET.Server.src.admin
{
    [ApiController]
    [Route("api/config")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DbConfigurationController : ControllerBase
    {

        private readonly IDbConfigurationService dbConfigurationService;

        public DbConfigurationController(IDbConfigurationService dbConfigurationService)
        {
            this.dbConfigurationService = dbConfigurationService;
        }

        [HttpPost]
        [Route("database/create")]
        [Authorize(Policy = "AuthorCreate")]
        public async Task<IActionResult> createDatabase([FromQuery] bool withData)
        {
            try
            {
                return Ok("Database created.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Failed to create database.");
            }
        }
    }
}
