﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        [Authorize(Policy = "AdminCreate")]
        public async Task<IActionResult> CreateDatabase([FromQuery] bool withData)
        {
            try
            {
                if (withData == false)
                {
                    await dbConfigurationService.CreateEmptyDatabaseAsync();
                } else
                {
                    await dbConfigurationService.CreateDatabaseWithExampleDataAsync();
                }
                return Ok("Database created.");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Failed to create database.");
            }
        }
    }
}
