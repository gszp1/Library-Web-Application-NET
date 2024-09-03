using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.exception;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.service;
using Library_Web_Application_NET.Server.src.service.interfaces;
using Library_Web_Application_NET.Server.src.util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Library_Web_Application_NET.Server.src.controller
{
    [Route("api/resources")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceService resourceService;

        private readonly IResourceInstanceService instanceService;

        public ResourceController(IResourceService resourceService, IResourceInstanceService instanceService)
        {
            
            this.resourceService = resourceService;
            this.instanceService = instanceService;
        }

        [AllowAnonymous]
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

        [Authorize(Policy = "AdminRead")]
        [HttpGet("admin/all")]
        public async Task<ActionResult<List<AdminResourceDto>>> GetAllAdmin()
        {
            return Ok(await resourceService.GetAllAdminAsync());
        }

        [AllowAnonymous]
        [HttpGet("all/paginated")]
        public async Task<ActionResult<List<ResourceDto>>> GetAllPaginated
        (
            [FromQuery] string? keyword,
            [FromQuery] int? page,
            [FromQuery] int? size 
        )
        {
            size = size.HasValue && size.Value > 0 ? size.Value : 10;
            page = page.HasValue && page.Value > 0 ? page.Value : 1;
            Pageable pageable = new Pageable()
            {
                PageSize = size.Value,
                PageNumber = page.Value
            };
            if (keyword.IsNullOrEmpty())
            {
                return Ok(await resourceService.GetAllWithAuthorsPageableAsync(pageable));
            }
            return Ok(await resourceService.GetResourcesWithKeywordInTitlePageableAsync(keyword, pageable));
        }

        [AllowAnonymous]
        [HttpGet("{id}/description")]
        public async Task<ActionResult<ResourceDescriptionDto>> GetDescription(int id)
        {
            return Ok(await resourceService.GetResourceDescriptionAsync(id));
        }

        [AllowAnonymous]
        [HttpGet("{id}/instances/notReserved")]
        public async Task<ActionResult<List<InstanceDto>>> GetNotReservedInstances(int id)
        {
            return Ok(await instanceService.GetNotReservedInstancesOfResourceAsync(id));
        }

        [Authorize(Policy = "AdminCreate")]
        [HttpPost("create")]
        public async Task<ActionResult<string>> CreateResource([FromBody] CreateResourceDto dto)
        {
            try
            {
                Resource resource = await resourceService.CreateResourceAsync(dto);
                return Ok(resource.ResourceId.ToString());
            }
            catch (InvalidDataException otae)
            {
                return BadRequest(otae.Message);
            }
        }

        [Authorize(Policy = "AdminUpdate")]
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
            catch (InvalidRequestDataException irde)
            {
                {
                    return BadRequest(irde.Message);
                }
            }

        }
    }
}
