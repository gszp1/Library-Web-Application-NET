using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.service;
using Microsoft.AspNetCore.Mvc;

namespace Library_Web_Application_NET.Server.src.controller
{
    [Route("api/publishers")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly PublisherService publisherService;

        public PublisherController(PublisherService publisherService)
        {
            this.publisherService = publisherService;
        }

        [HttpPost("create")]
        public ActionResult<string> Create([FromBody] PublisherDto dto)
        {

        }

        [HttpGet("all")]
        public ActionResult<List<AdminPublisherDto>> GetAll()
        {

        }

        [HttpPut("update")]
        public ActionResult<string> Update([FromBody] AdminPublisherDto dto)
        {

        }
    }
}
