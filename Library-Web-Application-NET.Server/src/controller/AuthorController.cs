using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.service;
using Microsoft.AspNetCore.Mvc;

namespace Library_Web_Application_NET.Server.src.controller
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService authorService;

        public AuthorController(AuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpPost("create")]
        public ActionResult<string> CreateAuthor([FromBody] AdminAuthorDto dto)
        {

        }

        [HttpGet("all")]
        public ActionResult<List<FullAuthorDto>> GetAllAuthors()
        {

        }

        [HttpPut("update")]
        public ActionResult<string> UpdateAuthor([FromBody] FullAuthorDto dto)
        {

        }
    }
}
