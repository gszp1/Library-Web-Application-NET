using Library_Web_Application_NET.Server.src.dto;
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
        public ActionResult<UserDto> GetUserByEmail(string email)
        {

        }

        [HttpGet("update")]
        public ActionResult<string> UpdateUserCredentials([FromBody] UserDto dto)
        {

        }

        [HttpGet("all")]
        public ActionResult<List<AdminAuthorDto>> GetAllByEmailKeyword([FromQuery] string? keyword) 
        {

        }

        [HttpGet("{id}")]
        public ActionResult<AdminUserDto> GetUserById(int id)
        {

        }

        [HttpPut("admin/update")]
        public ActionResult<string> UpdateUser([FromBody] AdminUserDto dto)
        {

        }
    }
}
