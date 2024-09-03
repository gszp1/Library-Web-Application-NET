using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.exception;
using Library_Web_Application_NET.Server.src.service;
using Library_Web_Application_NET.Server.src.service.interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library_Web_Application_NET.Server.src.controller
{
    [Route("api/instances")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ResourceInstanceController : ControllerBase
    {
        private readonly IResourceInstanceService resourceInstanceService;

        public ResourceInstanceController(IResourceInstanceService resourceInstanceService)
        {
            this.resourceInstanceService = resourceInstanceService;
        }

        [Authorize(Policy = "AdminRead")]
        [HttpGet("all/resource/{id}")]
        public async Task<ActionResult<List<AdminInstanceDto>>> GetAllForResource(int id)
        {
            return Ok(await resourceInstanceService.GetAllAdminInstancesByResourceIdAsync(id, "InstanceId", true));
        }

        [Authorize(Policy = "AdminUpdate")]
        [HttpGet("update")]
        public async Task<ActionResult<string>> UpdateInstance([FromBody] AdminInstanceDto dto)
        {
            try
            {
                await resourceInstanceService.UpdateInstanceAsync(dto);
                return Ok("Instance updated successfully.");
            }
            catch (NoSuchRecordException nere)
            {
                return NotFound(nere.Message);
            }
            catch (OperationNotAvailableException onae)
            {
                return BadRequest(onae.Message);
            }
        }

        [Authorize(Policy = "AdminUpdate")]
        [HttpPut("{id}/withdraw")]
        public async Task<ActionResult<string>> WithdrawInstance(int id)
        {
            try
            {
                await resourceInstanceService.WithdrawInstanceAsync(id);
                return Ok("Withdraw success.");
            }
            catch (NoSuchRecordException nere)
            {
                return NotFound(nere.Message);
            }
            catch (OperationNotAvailableException onae)
            {
                return BadRequest(onae.Message);
            }
        }

        [Authorize(Policy = "AdminCreate")]
        [HttpPost("create/{id}")]
        public async Task<ActionResult<string>> CreateInstance(int id)
        {
            try
            {
                await resourceInstanceService.CreateInstanceAsync(id);
                return Ok("Instance created successfully.");
            }
            catch (NoSuchRecordException nere)
            {
                return NotFound(nere.Message);
            }
        }
    }
}
