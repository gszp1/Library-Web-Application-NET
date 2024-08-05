using Library_Web_Application_NET.Server.src.dto;
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


        [HttpGet("all/resource/{id}")]
        public ActionResult<List<AdminInstanceDto>> GetAllForResource(int id)
        {

        }

        [HttpGet("update")]
        public ActionResult<string> UpdateInstance([FromBody] AdminInstanceDto dto)
        {

        }

        [HttpPut("{id}/withdraw")]
        public ActionResult<string> WithdrawInstance(int id)
        {

        }

        [HttpPost("create/{id}")]
        public ActionResult<string> CreateInstance(int id)
        {

        }
    }
}
