using Library_Web_Application_NET.Server.src.dto;
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

        [HttpGet("all")]
        public ActionResult<List<ResourceDto>> GetAll([FromQuery] string? keyword)
        {

        }

        [HttpGet("admin/all")]
        public ActionResult<List<AdminResourceDto>> GetAllAdmin()
        {

        }

        [HttpGet("all/paginated")]
        public ActionResult<List<ResourceDto>> GetAllPaginated([FromQuery] string? keyword)
        {
            // Do proper pagination
        }

        [HttpGet("{id}/description")]
        public ActionResult<ResourceDescriptionDto> GetDescription(int id) 
        {
        
        }

        [HttpGet("{id}/instances/notReserved")]
        public ActionResult<List<InstanceDto>> GetNotReservedInstances(int id)
        {

        }

        [HttpPost("create")]
        public ActionResult<string> CreateResource([FromBody] CreateResourceDto dto)
        {

        }

        [HttpPut("update")]
        public ActionResult<string> UpdateResource([FromBody] UpdateResourceDto dto)
        {

        }

    }
}
