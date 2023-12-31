﻿using API.Authentication;
using API.Data.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Policy = nameof(UserPermissions.ManageUsers))]
public sealed class AdminController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<AdminController> _logger;
    private readonly IMapper _mapper;

    public AdminController(
        UserManager<AppUser> userManager,
        ILogger<AdminController> logger,
        IMapper mapper)
    {
        _userManager = userManager;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpPost("users")]
    public async Task<ActionResult> AddNewUser([FromBody] NewUserDto userData)
    {
        var user = _mapper.Map<AppUser>(userData);
        user.UserName = userData.Email;
        
        var result = await _userManager.CreateAsync(user, userData.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);

        return Ok(_mapper.Map<AppUserDto>(user));
    }

    [HttpPut("users/{userId}")]
    public async Task<ActionResult> UpdateUserPermissions([FromBody] UpdateUserDto userData, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) return NotFound("No user found by that Id");

        _mapper.Map(user, userData);
        var result = await _userManager.UpdateAsync(user);
        
        if (result.Succeeded) return Ok(_mapper.Map<AppUserDto>(user));
    
        return BadRequest();
    }
}