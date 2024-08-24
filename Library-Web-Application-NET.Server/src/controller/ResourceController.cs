using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.exception;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.service;
using Library_Web_Application_NET.Server.src.util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
        public async Task<ActionResult<List<ResourceDto>>> GetAll([FromQuery] string? keyword)
        {
            if (keyword.IsNullOrEmpty())
            {
                return Ok(await resourceService.GetAllWithAuthorsAsync());
            } 
            else
            {
                return Ok(await resourceService.GetResourcesWithKeywordInTitleAsync(keyword));
            }
        }

        [HttpGet("admin/all")]
        public async Task<ActionResult<List<AdminResourceDto>>> GetAllAdmin()
        {
            return Ok(await resourceService.GetAllAdminAsync());
        }

        [HttpGet("all/paginated")]
        public async Task<ActionResult<List<ResourceDto>>> GetAllPaginated([FromQuery] string? keyword)
        {
        }

        [HttpGet("{id}/description")]
        public async Task<ActionResult<ResourceDescriptionDto>> GetDescription(int id) 
        {
            return Ok(await resourceService.GetResourceDescriptionAsync(id));
        }

        [HttpGet("{id}/instances/notReserved")]
        public async Task<ActionResult<List<InstanceDto>>> GetNotReservedInstances(int id)
        {
            
        }

        [HttpPost("create")]
        public async Task<ActionResult<string>> CreateResource([FromBody] CreateResourceDto dto)
        {
            try
            {
                Resource resource =  await resourceService.CreateResourceAsync(dto);
                return Ok(resource.ResourceId.ToString());
            }
            catch (InvalidDataException otae)
            {
                return BadRequest(otae.Message);
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<string>> UpdateResource([FromBody] UpdateResourceDto dto)
        {
            try
            {
                await resourceService.UpdateResourceAsync(dto);
                return Ok("Resource updated successfully.");
            }
            catch (NoSuchRecordException nsre)
            {
                return NotFound(nsre.Message);
            }
            catch (InvalidRequestDataException irde) {
            {
                return BadRequest(irde.Message);
            }
        }

    }
}
