using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.service;
using Library_Web_Application_NET.Server.src.service.interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Web_Application_NET.Server.src.controller
{
    [Route("api/authors")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService authorService;

        public AuthorController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [Authorize(Policy = "AdminCreate")]
        [HttpPost("create")]
        public async Task<ActionResult<string>> CreateAuthor([FromBody] AdminAuthorDto dto)
        {
            try
            {
                await authorService.CreateAuthorAsync(dto);
                return Ok("Author created.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Policy = "AdminRead")]
        [HttpGet("all")]
        public async Task<ActionResult<List<FullAuthorDto>>> GetAllAuthors()
        {
            return Ok(await authorService.GetAllAuthorsAsync());
        }

        [Authorize(Policy = "AdminUpdate")]
        [HttpPut("update")]
        public async Task<ActionResult<string>> UpdateAuthor([FromBody] FullAuthorDto dto)
        {
            try
            {
                await authorService.UpdateAuthorAsync(dto);
                return Ok("Author updated.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
