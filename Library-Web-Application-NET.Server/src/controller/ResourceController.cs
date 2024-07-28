using Library_Web_Application_NET.Server.src.service;
using Microsoft.AspNetCore.Mvc;

namespace Library_Web_Application_NET.Server.src.controller
{
    [Route("api/resources")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly ResourceService resourceService;

        public ResourceController(ResourceService resourceService)
        {
            this.resourceService = resourceService;
        }
    }
}
