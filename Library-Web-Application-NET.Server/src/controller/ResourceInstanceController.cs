using Library_Web_Application_NET.Server.src.service;
using Microsoft.AspNetCore.Mvc;

namespace Library_Web_Application_NET.Server.src.controller
{
    [Route("api/resourceInstances")]
    [ApiController]
    public class ResourceInstanceController : ControllerBase
    {
        private readonly ResourceInstanceService resourceInstanceService;

        public ResourceInstanceController(ResourceInstanceService resourceInstanceService)
        {
            this.resourceInstanceService = resourceInstanceService;
        }
    }
}
