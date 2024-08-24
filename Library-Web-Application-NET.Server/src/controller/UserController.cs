using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.service;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;

namespace Library_Web_Application_NET.Server.src.controller
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
       private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{email}/credentials")]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
        {
            UserDto? user = await userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpGet("update")]
        public async Task<ActionResult<string>> UpdateUserCredentials([FromBody] UserDto dto)
        {

        }

        [HttpGet("all")]
        public async Task<ActionResult<List<AdminAuthorDto>>> GetAllByEmailKeyword([FromQuery] string? keyword) 
        {

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminUserDto>> GetUserById(int id)
        {

        }

        [HttpPut("admin/update")]
        public async Task<ActionResult<string>> UpdateUser([FromBody] AdminUserDto dto)
        {

        }
    }
}
