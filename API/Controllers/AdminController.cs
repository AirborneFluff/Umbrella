using System.Security.Claims;
using API.Authentication;
using API.Data.DTOs;
using API.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Policy = "RequireOwnerRole")]
public sealed class AdminController : BaseApiController
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<AdminController> _logger;
    private readonly IMapper _mapper;

    public AdminController(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ILogger<AdminController> logger,
        IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpPost("users")]
    public async Task<ActionResult> AddNewUser([FromBody] NewUserDto userData)
    {
        var user = new IdentityUser()
        {
            UserName = userData.Email,
            Email = userData.Email
        };
        
        var result = await _userManager.CreateAsync(user, userData.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);

        foreach (var role in userData.Roles)
        {
            if (!await _roleManager.RoleExistsAsync(role)) continue;
            await _userManager.AddToRoleAsync(user, role);
        }

        var roles = await _userManager.GetRolesAsync(user);

        return Ok(new
        {
            Email = user.Email,
            Roles = roles
        });
    }
}