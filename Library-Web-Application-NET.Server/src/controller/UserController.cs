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

    }
}
