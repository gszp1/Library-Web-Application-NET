using Library_Web_Application_NET.Server.src.service;
using Microsoft.AspNetCore.Mvc;

namespace Library_Web_Application_NET.Server.src.controller
{
    [Route("api/authorResources")]
    [ApiController]
    public class AuthorResourceController : ControllerBase
    {
        private readonly AuthorResourceService authorResourceService;

        public AuthorResourceController(AuthorResourceService authorResourceService)
        {
            this.authorResourceService = authorResourceService;
        }
    }
}
