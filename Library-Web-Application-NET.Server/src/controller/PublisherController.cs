using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.exception;
using Library_Web_Application_NET.Server.src.service;
using Library_Web_Application_NET.Server.src.service.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library_Web_Application_NET.Server.src.controller
{
    [Route("api/publishers")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            this.publisherService = publisherService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<string>> Create([FromBody] PublisherDto dto)
        {
            try
            {
                await publisherService.CreatePublisherAsync(dto);
                return Ok("Publisher created.");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<AdminPublisherDto>>> GetAll()
        {
            return Ok(await publisherService.GetAllPublishersAsync());
        }

        [HttpPut("update")]
        public async Task<ActionResult<string>> Update([FromBody] AdminPublisherDto dto)
        {
            try
            {
                await publisherService.UpdatePublisherAsync(dto);
                return Ok("Publisher updated.");
            } 
            catch (NoSuchRecordException ex)
            {
                return NotFound(ex.Message);
            }
            catch (OperationNotAvailableException onae)
            {
               return BadRequest(onae.Message);
            }
        }
    }
}
