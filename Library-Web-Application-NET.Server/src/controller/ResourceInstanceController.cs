using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.exception;
using Library_Web_Application_NET.Server.src.service;
using Library_Web_Application_NET.Server.src.service.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library_Web_Application_NET.Server.src.controller
{
    [Route("api/resourceInstances")]
    [ApiController]
    public class ResourceInstanceController : ControllerBase
    {
        private readonly IResourceInstanceService resourceInstanceService;

        public ResourceInstanceController(IResourceInstanceService resourceInstanceService)
        {
            this.resourceInstanceService = resourceInstanceService;
        }


        [HttpGet("all/resource/{id}")]
        public async Task<ActionResult<List<AdminInstanceDto>>> GetAllForResource(int id)
        {
            return Ok(await resourceInstanceService.GetAllAdminInstancesByResourceIdAsync(id, "InstanceId", true));
        }

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
