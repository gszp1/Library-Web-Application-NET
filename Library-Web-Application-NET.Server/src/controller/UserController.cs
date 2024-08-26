﻿using Library_Web_Application_NET.Server.src.dto;
using Library_Web_Application_NET.Server.src.exception;
using Library_Web_Application_NET.Server.src.model;
using Library_Web_Application_NET.Server.src.service;
using Library_Web_Application_NET.Server.src.service.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.ConstrainedExecution;

namespace Library_Web_Application_NET.Server.src.controller
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
       private readonly IUserService userService;

        public UserController(IUserService userService)
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
            try
            {
                await userService.UpdateUserCredentialsAsync(dto);
                return Ok("User updated successfully.");
            }
            catch (NoSuchRecordException nsre)
            {
                return NotFound(nsre.Message);    
            }
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<AdminAuthorDto>>> GetAllByEmailKeyword([FromQuery] string? keyword) 
        {
            if (keyword.IsNullOrEmpty())
            {
                return Ok(await userService.FindAllAsync());
            }
            return Ok(await userService.FindAllByEmailKeywordAsync(keyword));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminUserDto>> GetUserById(int id)
        {
            return Ok(await userService.FindByIdAsync(id));
        }

        [HttpPut("admin/update")]
        public async Task<ActionResult<string>> UpdateUser([FromBody] AdminUserDto dto)
        {
            try
            {
                await userService.UpdateUserAsync(dto);
                return Ok("User updated successfully.");
            }
            catch (NoSuchRecordException nsre)
            {
                return NotFound(nsre.Message);
            }
        }
    }
}
